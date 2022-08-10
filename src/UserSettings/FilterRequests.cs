using Guessr.Models;
using static Guessr.UserSettings.Settings;

namespace Guessr.UserSettings;

internal static class FilterRequests
{
    public static string Options;

    public static void Filter(ApiToken token)
    {
        Options = "?amount=1";

        var responseType = ChoiceType.GetCurrent();
        var difficulty = Difficulty.GetCurrent();
        var category = Category.GetCurrent();

        if (responseType != TriviaResponseType.All)
        {
            Options += "&type=" + responseType.ToString().ToLower();
        }

        if (difficulty != TriviaDifficulty.All)
        {
            Options += "&difficulty=" + difficulty.ToString().ToLower();
        }

        if (category.Id != 0)
        {
            Options += "&category=" + category.Id;
        }

        if (token is not null)
        {
            Options += "&token=" + token.Token;
        }
    }
}