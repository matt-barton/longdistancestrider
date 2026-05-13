using LDS.Data.Services;
using LDS.Data.Services.Interfaces;

namespace LDS.Web.Shared.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddLdsDataServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IParametersService, ParametersService>();
        builder.Services.AddTransient<IRaceEntryService, RaceEntryService>();
        builder.Services.AddTransient<IRaceParticipationService, RaceParticipationService>();
        builder.Services.AddTransient<IRaceService, RaceService>();
        builder.Services.AddTransient<IRunnerService, RunnerService>();
        builder.Services.AddTransient<IRunnerAliasService, RunnerAliasService>();
        builder.Services.AddTransient<ITotalMilesService, TotalMilesService>();

        return builder;
    }
}