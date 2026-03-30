using LDS.Data.Services;
using LDS.Data.Services.Interfaces;

namespace LDS.Web.Shared.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddLdsServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRunnerService, RunnerService>();
        builder.Services.AddTransient<IRaceService, RaceService>();
        builder.Services.AddTransient<IRaceEntryService, RaceEntryService>();
        builder.Services.AddTransient<IRaceParticipationService, RaceParticipationService>();
        builder.Services.AddTransient<ITotalMilesService, TotalMilesService>();
        builder.Services.AddTransient<IParametersService, ParametersService>();

        return builder;
    }
}