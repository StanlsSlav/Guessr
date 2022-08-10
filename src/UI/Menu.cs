using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Guessr.Models;
using Guessr.UI.Menus;
using Guessr.UserSettings;
using static System.Text.Json.JsonSerializer;
using static Guessr.ColorFeedBack;
using static Guessr.UserSettings.FilterRequests;
using static Guessr.Parsing.ParseApiToken;

namespace Guessr.UI;

internal static class Menu
{
    private static MenuType _currentMenuType = MenuType.Main;
    private static TimeSpan _waitTime = TimeSpan.FromSeconds(0.75);

    private static bool _wasTokenLoaded;

    /// <summary>
    ///     Exit the program
    /// </summary>
    public static readonly Action<int> Exit = exitCode =>
    {
        Console.Clear();
        Colored("GoodBye!", foreground: ConsoleColor.Cyan);
        Environment.Exit(exitCode);
    };

    /// <summary>
    ///     Renders the menu with its options
    ///     Handles the user input
    /// </summary>
    public static async Task HandleMenuInputAsync()
    {
        if (!_wasTokenLoaded)
        {
            LoadToken();
            _wasTokenLoaded = true;
        }

        _waitTime = TimeSpan.FromSeconds(0.75);

        Console.Clear();
        RenderCurrentMenu();

        Console.Write("\nInput: ");
        var userInput = int.TryParse(Console.ReadLine(), out var input) ? input : default;

        switch (_currentMenuType)
        {
            default:
            case MenuType.Main:
                switch (userInput)
                {
                    case 1:
                        Console.Clear();
                        Filter(ParsedApiToken);
                        await StartTrivia.Start();
                        break;
                    case 2:
                        _currentMenuType = MenuType.Settings;
                        break;
                    case 3:
                        _currentMenuType = MenuType.Token;
                        break;
                    case 4:
                        Exit(0);
                        break;
                }

                break;

            case MenuType.Settings:
                switch (userInput)
                {
                    case 1:
                        Settings.Category.Next();
                        break;
                    case 2:
                        Settings.Difficulty.Next();
                        break;
                    case 3:
                        Settings.ChoiceType.Next();
                        break;
                    case 4:
                        _currentMenuType = MenuType.Main;
                        break;
                }

                break;

            case MenuType.Token:
                switch (userInput)
                {
                    case 1:
                        await RetrieveToken();
                        Colored("Retrieved the token " + ParsedApiToken.Token);
                        break;
                    case 2:
                        await ResetToken();
                        Colored("Reset token " + ParsedApiToken.Token);
                        break;
                    case 3:
                        var doesTokenExist = File.Exists("token.json");

                        // Possible drawback, check status on file rather than object
                        var leftTime = doesTokenExist
                                           ? Deserialize<ApiToken>(await File.ReadAllTextAsync("token.json"))!
                                                 .RequestDate
                                                 .AddHours(6) -
                                             DateTime.Now
                                           : TimeSpan.FromSeconds(0);

                        Colored("Token: " + ParsedApiToken.Token);
                        Colored(
                            "Available to use: " + doesTokenExist,
                            foreground: doesTokenExist ? ConsoleColor.Green : ConsoleColor.Red
                        );
                        Colored("Time left: " + leftTime);
                        _waitTime = TimeSpan.FromSeconds(2.5); // Extra time to appraise the token infos
                        break;
                    case 4:
                        Colored("Opening the path with the token file");
                        Process.Start("cmd.exe", "/c explorer " + Environment.CurrentDirectory);
                        break;
                    case 5:
                        _currentMenuType = MenuType.Main;
                        break;
                }

                break;
        }

        await Task.Delay(_waitTime);
        await HandleMenuInputAsync();
    }

    private static void RenderCurrentMenu()
    {
        switch (_currentMenuType)
        {
            case MenuType.Main:
                MainBaseMenu.GetInstance().Render();
                break;

            case MenuType.Settings:
                SettingsBaseMenu.GetInstance().Render();
                break;

            case MenuType.Token:
                TokenBaseMenu.GetInstance().Render();
                break;

            default: return;
        }
    }

    private enum MenuType
    {
        Main,
        Settings,
        Token
    }
}