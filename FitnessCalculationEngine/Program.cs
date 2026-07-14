using FitnessCalculationEngine;
using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Features.Calculations;
using FitnessCalculationEngine.Features.FitnessStats;
using FitnessCalculationEngine.Features.Metrics;
using FitnessCalculationEngine.Features.Plans;
using FitnessCalculationEngine.Features.Recalculation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFce(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/health", () => Results.Ok(ApiResponse<object>.Ok(new { status = "healthy" }, "Fitness Calculation Engine Service is up")));

app.MapFitnessStatsEndpoints();
app.MapCalculationsEndpoints();
app.MapPlansEndpoints();
app.MapMetricsEndpoints();
app.MapRecalculationEndpoints();

app.Run();
