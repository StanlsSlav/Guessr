using System.Threading.Tasks;

namespace OpenTriviaAPICaller
{
	class ProcessInput
	{
		/// <summary>
		/// Checks if the user input is equal to the correct answer
		/// Or if a custom command was inputted
		/// </summary>
		public static async Task<bool> CheckIfCorrect(string input)
		{
			//Custom console commands during the game
			switch (input)
			{
				case "quit" or "q": Menu._QuitMessage(); break;
				case "back" or "b": await Task.Run(() => Menu.Render()); break;
				default: break;
			}

			//CapsLock forgotten
			return input.ToLower() == ParseRequest._Trivia.CorrectAnswer.ToLower();
		}
	}
}
