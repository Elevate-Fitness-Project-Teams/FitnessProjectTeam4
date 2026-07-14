using AuthService;
using AuthService.Common.Api;
using AuthService.Features.CompleteProfile;
using AuthService.Features.ForgotPassword;
using AuthService.Features.Login;
using AuthService.Features.Logout;
using AuthService.Features.RefreshToken;
using AuthService.Features.Register;
using AuthService.Features.ResetPassword;
using AuthService.Features.VerifyOtp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/health", () => Results.Ok(ApiResponse<object>.Ok(new { status = "healthy" }, "Authentication Service is up")));

app.MapRegisterEndpoint();
app.MapCompleteProfileEndpoint();
app.MapLoginEndpoint();
app.MapForgotPasswordEndpoint();
app.MapVerifyOtpEndpoint();
app.MapResetPasswordEndpoint();
app.MapRefreshTokenEndpoint();
app.MapLogoutEndpoint();

app.Run();
