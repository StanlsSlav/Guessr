using System;
using OpenTriviaAPICaller.DataModels;
using static OpenTriviaAPICaller.UserSettings.UserOptions;

namespace OpenTriviaAPICaller.UserSettings
{
    internal static class FilterRequests
    {
        public static string Options;

        public static void Filter(ApiToken token)
        {
            Options = "?amount=1";

            if (UserOptions.Type != 0)
                Options += "&type=" + Enum.GetName(typeof(TypeChoices), UserOptions.Type)?.ToLower();

            else if (Difficulty != 0)
                Options += "&difficulty=" + Enum.GetName(typeof(DifficultyChoices), Difficulty)?.ToLower();

            else if (Category != 0)
                Options += "&category=" + (Category + 9); //Offset

            else if (token is not null)
                Options += "&token=" + token.Token;
        }
    }
}