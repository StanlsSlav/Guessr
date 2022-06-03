using System;
using Guessr.Models;
using static Guessr.UserSettings.UserOptions;

namespace Guessr.UserSettings;

internal static class FilterRequests
{
    public static string Options;

    public static void Filter(ApiToken token)
    {
        Options = "?amount=1";

        if (UserOptions.Type != 0)
        {
            Options += "&type=" + Enum.GetName(typeof(TypeChoices), UserOptions.Type)?.ToLower();
        }
        else if (Difficulty != 0)
        {
            Options += "&difficulty=" + Enum.GetName(typeof(DifficultyChoices), Difficulty)?.ToLower();
        }
        else if (Category != 0)
        {
            //Offset
            Options += "&category=" + (Category + 9);
        }
        else if (token is not null)
        {
            Options += "&token=" + token.Token;
        }
    }
}