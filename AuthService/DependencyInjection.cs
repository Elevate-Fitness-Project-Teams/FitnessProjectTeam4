using AuthService.Common;
using AuthService.Common.Api;
using AuthService.Common.BackgroundJobs;
using AuthService.Common.Email;
using AuthService.Common.Messaging;
using AuthService.Common.Persistence;
using AuthService.Common.Security;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;

namespace AuthService;

public static class DependencyInjection
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AuthDbContext>(opts =>
            opts.UseSqlServer(config.GetConnectionString("AuthDb")));

        services.AddScoped<IRepository, Repository>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.Configure<JwtOptions>(config.GetSection("Jwt"));
        services.Configure<SmtpOptions>(config.GetSection("Smtp"));
        services.Configure<LockoutOptions>(config.GetSection("Lockout"));
        services.Configure<OtpOptions>(config.GetSection("Otp"));

        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        var smtpProbe = config.GetSection("Smtp").Get<SmtpOptions>() ?? new SmtpOptions();
        if (!string.IsNullOrWhiteSpace(smtpProbe.Host) &&
            !string.IsNullOrWhiteSpace(smtpProbe.User) &&
            !string.IsNullOrWhiteSpace(smtpProbe.Pass))
        {
            services.AddSingleton<IEmailSender, SmtpEmailSender>();
        }
        else
        {
            services.AddSingleton<IEmailSender, ConsoleEmailSender>();
        }

        services.AddAuthMessaging(config);

        var jwt = config.GetSection("Jwt").Get<JwtOptions>()
                  ?? throw new InvalidOperationException("Jwt configuration section missing.");
        if (string.IsNullOrWhiteSpace(jwt.Key) || jwt.Key.Length < 32)
            throw new InvalidOperationException("Jwt:Key must be at least 32 characters.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        services.AddAuthorization();

        services.AddRateLimiter(o =>
        {
            o.RejectionStatusCode = 429;
            o.AddPolicy("login", ctx => RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromMinutes(1),
                    QueueLimit = 0
                }));
            o.AddPolicy("forgot-password", ctx => RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 5,
                    Window = TimeSpan.FromMinutes(1),
                    QueueLimit = 0
                }));
        });

        services.AddHostedService<LoginAttemptsPurgeService>();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "Elevate Authentication Service",
                Version = "v1",
                Description = "Owns credentials, sessions, token rotation, lockout, and OTP recovery."
            });
        });

        return services;
    }
}
