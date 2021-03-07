using System;
using System.Threading.Tasks;
using static System.Console;
using static System.ConsoleColor;
using static OpenTrivia.ParseRequest;
using static OpenTrivia.ColorFeedBack;

namespace OpenTrivia
{
	class StartTrivia
	{
		/* Game UI */

		public static async Task Start()
		{
			CursorVisible = false;

			while (true)
			{
				await ParseQuestion();

				///<placesTillCorrect>Sets the place of the right answer as an index</placesTillCorrect>
				///<correctChoiceNr>Save the correct choice number as an non index</correctChoiceNr>
				///<toRest>Fix the repeating of the last wrong answer</toRest>
				var placesTillCorrect = new Random().Next(0, Quizz.IncorrectAnswers.Count + 1);
				var correctChoiceNr = placesTillCorrect + 1;
				var toRest = 0;

				WriteLine(Quizz.Question + '\n');
				var showPlaceNr = 1;
				for (int i = 0; i <= Quizz.IncorrectAnswers.Count; i++)
				{
					var isPlaceForCorrect = placesTillCorrect == 0;

					Colored(
						input: isPlaceForCorrect ? Quizz.CorrectAnswer.TrimStart() : Quizz.IncorrectAnswers[i - toRest],
						prefixToColor: showPlaceNr + ")",
						foreground: DarkYellow
					);

					if (isPlaceForCorrect) toRest++;
					placesTillCorrect--;
					showPlaceNr++;
				}

				Write("\nAnswer: ");
				string userInput = ReadLine();

				//Count as correct if the input is a number and it's equal to the correct answer
				if (int.TryParse(userInput, out int userIntInput) && userIntInput == correctChoiceNr)
					userInput = Quizz.CorrectAnswer;

				if (await ProcessInput.CheckIfCorrect(userInput))
					Colored("Correct!", foreground: Green);

				else
					Colored("Incorrect", foreground: Red);

				await Task.Delay(TimeSpan.FromSeconds(1.5));
				Clear();
			}
		}
	}
}
