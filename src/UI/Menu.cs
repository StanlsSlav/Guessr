using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using OpenTriviaAPICaller.DataModels;
using OpenTriviaAPICaller.UserSettings;
using static System.Text.Json.JsonSerializer;
using static OpenTriviaAPICaller.ColorFeedBack;
using static OpenTriviaAPICaller.UserSettings.FilterRequests;
using static OpenTriviaAPICaller.DataParsing.ParseApiToken;

namespace OpenTriviaAPICaller.UI
{
    internal static class Menu
    {
        private static Menus _currentMenu = Menus.Main;
        private static readonly int TotalDifficultyChoices = Enum.GetValues(typeof(DifficultyChoices)).Length;
        private static readonly int TotalTypeChoices = Enum.GetValues(typeof(TypeChoices)).Length;
        private static TimeSpan _waitTime = TimeSpan.FromSeconds(0.75);

        /// <summary>
        ///     Exit the program
        /// </summary>
        public static readonly Action<int> Exit = exitCode =>
        {
            Console.Clear();
            Colored(
                "GoodBye!",
                foreground: ConsoleColor.Cyan);
            Environment.Exit(exitCode);
        };

        /// <summary>
        ///     Renders the menu with its options
        ///     Handles the user input
        /// </summary>
        public static async Task Render()
        {
            LoadToken();

            renderStart:
            _waitTime = TimeSpan.FromSeconds(0.75);

            Console.Clear();
            RenderMenu();

            Console.Write("\nInput: ");
            var userInput = Console.ReadLine();

            switch (_currentMenu)
            {
                case Menus.Main:
                    switch (userInput)
                    {
                        case "1":
                            Console.Clear();
                            Filter(ParsedToken);
                            await StartTrivia.Start();
                            break;
                        case "2":
                            _currentMenu = Menus.Settings;
                            break;
                        case "3":
                            _currentMenu = Menus.Token;
                            break;
                        case "4":
                            Exit(0);
                            break;
                        default: goto renderStart;
                    }

                    break;

                case Menus.Settings:
                    switch (userInput)
                    {
                        case "1":
                            UserOptions.Category += 1;
                            Colored("Category set to " + Enum.GetName(typeof(CategoryChoices), UserOptions.Category)
                                .Replace('_', ' '));
                            break;
                        case "2":
                            UserOptions.Difficulty = (UserOptions.Difficulty + 1) % TotalDifficultyChoices;
                            Colored("Difficulty set to " +
                                    Enum.GetName(typeof(DifficultyChoices), UserOptions.Difficulty));
                            break;
                        case "3":
                            UserOptions.Type = (UserOptions.Type + 1) % TotalTypeChoices;
                            Colored("Questions type set to " + Enum.GetName(typeof(TypeChoices), UserOptions.Type));
                            break;
                        case "4":
                            _currentMenu = Menus.Main;
                            break;
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
                            Colored("Reset token " + ParsedToken.Token);
                            break;
                        case "3":
                            var doesTokenExist = File.Exists("token.json");

                            // Possible drawback, check status on file rather than object
                            var leftTime = doesTokenExist
                                ? Deserialize<ApiToken>(await File.ReadAllTextAsync("token.json")).RequestDate
                                      .AddHours(6) -
                                  DateTime.Now
                                : TimeSpan.FromSeconds(0);

                            Colored("Token: " + ParsedToken.Token ?? "Null");
                            Colored(
                                "Available to use: " + doesTokenExist,
                                foreground: doesTokenExist ? ConsoleColor.Green : ConsoleColor.Red
                            );
                            Colored("Time left: " + leftTime);
                            _waitTime = TimeSpan.FromSeconds(2.5); //Extra time to appraise the token infos
                            break;
                        case "4":
                            Colored("Opening the path with the token file");
                            Process.Start("cmd.exe", "/c explorer " + Environment.CurrentDirectory);
                            break;
                        case "5":
                            _currentMenu = Menus.Main;
                            break;
                        default: goto renderStart;
                    }

                    break;
            }

            await Task.Delay(_waitTime);
            goto renderStart;
        }

        /// <summary>
        ///     Shows the custom menu's header with its content, options
        /// </summary>
        private static void RenderMenu()
        {
            List<string> menuOptions = new();

            switch (_currentMenu)
            {
                case Menus.Main:
                    Console.WriteLine("Welcome to Guessr!\nA trivia game based on OpenTrivia API\n\n");
                    menuOptions = new List<string> {"Start", "Settings", "Token", "Exit"};
                    break;

                case Menus.Settings:
                    Console.WriteLine("Settings Menu\n\n");
                    menuOptions = new List<string> {"Category", "Difficulty", "Type", "Back"};
                    break;

                case Menus.Token:
                    Console.WriteLine("Token Handling Menu\n\n");
                    menuOptions = new List<string> {"Retrieve", "Reset", "Status", "Open Explorer", "Back"};
                    break;
            }

            for (var i = 0; i < menuOptions.Count; i++)
            {
                var currentNr = i + 1;

                Colored(
                    input: menuOptions[i],
                    prefixToColor: currentNr + ")"
                );
            }
        }

        private enum Menus
        {
            Main,
            Settings,
            Token
        }
    }
}