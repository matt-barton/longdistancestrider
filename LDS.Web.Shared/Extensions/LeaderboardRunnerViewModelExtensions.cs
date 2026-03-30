using LDS.Web.Shared.ViewModels;

namespace LDS.Web.Shared.Extensions;

public static class LeaderboardRunnerViewModelExtensions
{
    public static IEnumerable<LeaderboardRunnerViewModel> WithPositions(
        this IEnumerable<LeaderboardRunnerViewModel> runners)
    {
        var count = 0;
        var position = 0;
        decimal last = 0;
        foreach (var r in runners)
        {
            count++;
            if (r.Miles == last)
            {
                r.Position = position + "=";
            }
            else
            {
                position = count;
                r.Position = position.ToString();
            }
            last = r.Miles;
        }

        return runners;
    }
}