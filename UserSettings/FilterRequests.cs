using System;

namespace OpenTriviaAPICaller
{
	class FilterRequests
	{
		public static string Options;
		public static void Filter(UserSettings setting, APIToken token)
		{
			Options = "?amount=1";

			if (setting.Type != 0)
				Options += "&type=" + Enum.GetName(typeof(TypeChoices), setting.Type).ToLower();

			else if (setting.Difficulty != 0)
				Options += "&difficulty=" + Enum.GetName(typeof(DifficultyChoices), setting.Difficulty).ToLower();

			else if (setting.Category != 0)
				Options += "&category=" + (setting.Category + 9); //Offset

			else if (token is not null)
				Options += "&token=" + token.Token;
		}
	}
}
