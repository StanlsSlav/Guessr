using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using static OpenTriviaAPICaller.ColorFeedBack;
using static OpenTriviaAPICaller.FilterRequests;
using static OpenTriviaAPICaller.ParseAPIToken;
using static System.Console;

namespace OpenTriviaAPICaller
{
	class Menu
	{
		private static Menus _CurrentMenu = Menus.Main;
		private static readonly UserSettings Settings = new();
		private static readonly int _TotalDifficultyChoices = Enum.GetValues(typeof(DifficultyChoices)).Length;
		private static readonly int _TotalTypeChoices = Enum.GetValues(typeof(TypeChoices)).Length;
		private static TimeSpan _WaitTime = TimeSpan.FromSeconds(0.75);

		/// <summary>
		/// A snippet to show the small quit message
		/// </summary>
		public static Action _QuitMessage = () =>
		{
			Clear();
			Colored(
				input:"GoodBye!",
				foreground: ConsoleColor.Cyan);
			Environment.Exit(1);
		};

		/// <summary>
		/// Renders the menu with its options
		/// Handles the user input
		/// </summary>
		public static async Task Render()
		{
			LoadToken();

			renderStart:
			_WaitTime = TimeSpan.FromSeconds(0.75);

			Clear();
			RenderMenu();

			Write("\nInput: ");
			string userInput = ReadLine();

			switch (_CurrentMenu)
			{
				case Menus.Main:
					switch (userInput)
					{
						case "1": Clear(); Filter(Settings, ParsedToken); await StartTrivia.Start(); break;
						case "2": _CurrentMenu = Menus.Settings; break;
						case "3": _CurrentMenu = Menus.Token; break;
						case "4": _QuitMessage(); break;
						default: goto renderStart;
					}
					break;

				case Menus.Settings:
					switch (userInput)
					{
						case "1":
							Settings.Category += 1;
							Colored("Category set to " + Enum.GetName(typeof(CategoryChoices), Settings.Category).Replace('_', ' '));
							break;
						case "2":
							Settings.Difficulty = (Settings.Difficulty + 1) % _TotalDifficultyChoices;
							Colored("Difficulty set to " + Enum.GetName(typeof(DifficultyChoices), Settings.Difficulty));
							break;
						case "3":
							Settings.Type = (Settings.Type + 1) % _TotalTypeChoices;
							Colored("Questions type set to " + Enum.GetName(typeof(TypeChoices), Settings.Type));
							break;
						case "4": _CurrentMenu = Menus.Main; break;
						default: goto renderStart;
					}
					break;

				case Menus.Token:
					switch (userInput)
					{
						case "1":
							await RetrieveToken();
							Colored("Retrieved the token " + ParsedToken.Token);
							break;
						case "2":
							await ResetToken();
							Colored("Resetted token " + ParsedToken.Token);
							break;
						case "3":
							var leftTime = ParsedToken.RequestDate.AddHours(6) - DateTime.Now;
							Colored("Token: " + ParsedToken.Token);

							Colored(
								input: "Available to use: " + (leftTime.TotalHours > 0),
								foreground: leftTime.TotalHours > 0 ? ConsoleColor.Green : ConsoleColor.Red
							);

							Colored("Time left: " + leftTime);
							_WaitTime = TimeSpan.FromSeconds(3); //Extra time to appraise the token infos
							break;
						case "4":
							Colored("Opening the path with the token file");
							System.Diagnostics.Process.Start("cmd.exe", "/c explorer " + Environment.CurrentDirectory);
							break;
						case "5": _CurrentMenu = Menus.Main; break;
						default: goto renderStart;
					}
					break;
			}
			
			await Task.Delay(_WaitTime);
			goto renderStart;
		}

		/// <summary>
		/// Shows the custom menu's header with its content, options
		/// </summary>
		private static void RenderMenu()
		{
			List<string> menuOptions = new();

			switch (_CurrentMenu)
			{
				case Menus.Main:
					WriteLine("Welcome to Guessr!\nA trivia game based on OpenTrivia API\n\n");
					menuOptions = new() { "Start", "Settings", "Token", "Exit" };
					break;

				case Menus.Settings:
					WriteLine("Settings Menu\n\n");
					menuOptions = new() { "Category", "Difficulty", "Type", "Back" };
					break;

				case Menus.Token:
					WriteLine("Token Handling Menu\n\n");
					menuOptions = new() { "Retrieve", "Reset", "Status", "Open Explorer", "Back" };
					break;
				default:
					break;
			}

			for (int i = 0; i < menuOptions.Count; i++)
			{
				var currentNr = i + 1;

				Colored(
				input: menuOptions[i],
				prefixToColor: (currentNr) + ")");
			}
		}

		enum Menus
		{
			Main,
			Settings,
			Token
		}
	}
}
