using System;
using System.Threading.Tasks;
using static OpenTriviaAPICaller.ParseRequest;
using static OpenTriviaAPICaller.ProcessInput;
using static OpenTriviaAPICaller.ColorFeedBack;
using static System.Console;
using static System.ConsoleColor;

namespace OpenTriviaAPICaller
{
	class StartTrivia
	{
		/// <summary>
		/// Game UI
		/// </summary>
		public static async Task Start()
		{
			CursorVisible = false;

			while (true)
			{
				await ParseQuestion();
				var placesTillCorrect = new Random().Next(1, _Trivia.IncorrectAnswers.Count + 2);
				var correctChoiceNr = placesTillCorrect;

				WriteLine(_Trivia.Question + '\n');

				int showChoiceNr = 1;
				for (int i = 0; i <= _Trivia.IncorrectAnswers.Count; i++)
				{
					placesTillCorrect--;

					if (placesTillCorrect == 0)
					{
						Colored(
							input: _Trivia.CorrectAnswer.TrimStart(),
							prefixToColor: $"{correctChoiceNr})", //Always show the correct choice
							foreground: DarkYellow
						);
					}
					else
					{
						/* ====================== Bug =======================
						 * Repeats the same question on placesTillCorrect > 2
						 *               As of i is 0 twice                */
						Colored(
							input: _Trivia.IncorrectAnswers[i == 0 ? i : i - 1],
							prefixToColor: $"{showChoiceNr})",
							foreground: DarkYellow
						);
					}

					showChoiceNr++;
				}

				Write("\nAnswer: ");
				string userInput = ReadLine();

				//Count as correct if the input is a number and it's equal to the correct answer
				if (int.TryParse(userInput, out int userIntInput) && userIntInput == correctChoiceNr)
					userInput = _Trivia.CorrectAnswer;

				if (await CheckIfCorrect(userInput))
					Colored("Correct!", foreground: Green);

				else
					Colored("Incorrect", foreground: Red);

				await Task.Delay(TimeSpan.FromSeconds(1.5));
				Clear();
			}
		}
	}
}
