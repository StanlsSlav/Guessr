using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using static System.Console;
using static System.ConsoleColor;
using static OpenTrivia.ColorFeedBack;
using static OpenTrivia.ParseAPIToken;

namespace OpenTrivia
{
	class ErrorsDictionary
	{
		private static readonly Dictionary<int, string[]> _Error = new()
		{
			[1] = new string[2] { "No Results", "Could not return results. The API doesn\'t have enough questions for your query." },
			[2] = new string[2] { "Invalid Parameter", "Contains an invalid parameter. Arguements passed in aren\'t valid" },
			[3] = new string[2] { "Token Not Found", "Session Token does not exist"},
			[4] = new string[2] { "Token Empty", "Session Token has returned all possible questions for the specified query. Resetting the Token is necessary"}
		};

		public static async Task HandleResponseCode(int responseCode)
		{
			if (responseCode == 0) return; //Code 0 is an all clear
			var isTokenError = responseCode == 3 || responseCode == 4;

			showError:
			Colored(
				input: $"ERROR ENCOUNTERED! Continue? (Y/N)\n\t{_Error[responseCode][0]} -- {_Error[responseCode][1]}\n",
				foreground: Red
			);

			//
			if (isTokenError)
			{
				Colored(
					input: "Retrieve a token? (R)",
					foreground: Yellow
				);
			}

			//Continue or bail?
			switch (ReadKey(true).Key)
			{
				case ConsoleKey.Y:
					Clear(); break;

				case ConsoleKey.N:
					Menu._QuitMessage(); break;

				case ConsoleKey.R:
					if (isTokenError)
						await RetrieveToken(); break;

				default:
					Clear(); goto showError;
			}
		}
	}
}
