﻿using System;
using OpenTriviaAPICaller.src.DataModels;

namespace OpenTriviaAPICaller.src.UserSettings
{
    internal static class FilterRequests
    {
        public static string Options;

        public static void Filter(DataModels.UserSettings setting, ApiToken token)
        {
            Options = "?amount=1";

            if (setting.Type != 0)
                Options += "&type=" + Enum.GetName(typeof(TypeChoices), setting.Type)?.ToLower();

            else if (setting.Difficulty != 0)
                Options += "&difficulty=" + Enum.GetName(typeof(DifficultyChoices), setting.Difficulty)?.ToLower();

            else if (setting.Category != 0)
                Options += "&category=" + (setting.Category + 9); //Offset

            else if (token is not null)
                Options += "&token=" + token.Token;
        }
    }
}