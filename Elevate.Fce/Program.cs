using Elevate.Fce;
using Elevate.Fce.Common.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFce(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Health endpoint — proves the app boots and the response envelope works.
app.MapGet("/health", () => Results.Ok(ApiResponse<object>.Ok(new { status = "healthy" }, "FCE is up")));

// Feature endpoints will be mapped from slices under /Features later.

app.Run();
