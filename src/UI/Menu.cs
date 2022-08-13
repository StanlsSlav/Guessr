using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Guessr.UI.Menus;
using Guessr.UserSettings;
using Spectre.Console;
using static Guessr.UserSettings.FilterRequests;
using static Guessr.Parsing.ParseApiToken;

namespace Guessr.UI;

internal static class Menu
{
    private static MenuType _currentMenuType = MenuType.Main;
    private static TimeSpan _waitTime;

    private static bool _wasTokenLoaded;

    /// <summary>
    ///     Exit the program
    /// </summary>
    public static readonly Action<int> Exit = exitCode =>
    {
        AnsiConsole.Clear();
        AnsiConsole.WriteLine("GoodBye!");
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
            await LoadTokenAsync();
            _wasTokenLoaded = true;
        }

        _waitTime = TimeSpan.FromSeconds(0.75);

        AnsiConsole.Clear();
        var userInput = GetSelectedOptionAndRenderCurrentMenu();

        switch (_currentMenuType)
        {
            default:
            case MenuType.Main:
                switch (userInput)
                {
                    case 1:
                        AnsiConsole.Clear();
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
                        AnsiConsole.MarkupLine($"Retrieved the token [cyan]{ParsedApiToken.Token}[/]");
                        break;
                    case 2:
                        await ResetToken();
                        AnsiConsole.MarkupLine($"Reset token [cyan]{ParsedApiToken.Token}[/]");
                        break;
                    case 3:
                        var isTokenValid = IsTokenValid();
                        var tokenValidityColor = isTokenValid ? "green" : "red";

                        AnsiConsole.MarkupLine($"Token: [cyan]{ParsedApiToken.Token}[/]");
                        AnsiConsole.MarkupLine($"Available to use: [{tokenValidityColor}]{isTokenValid}[/]");
                        AnsiConsole.MarkupLine($"Time left: [{tokenValidityColor}]{ParsedApiToken.LeftTime}[/]");

                        // Extra time to appraise the token infos
                        _waitTime = TimeSpan.FromSeconds(2.5);
                        break;
                    case 4:
                        AnsiConsole.WriteLine("Opening the path with the token file");
                        Process.Start("cmd.exe", $"/c explorer {Environment.CurrentDirectory}");
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

    private static int GetSelectedOptionAndRenderCurrentMenu()
    {
        return _currentMenuType switch
        {
            MenuType.Main     => MainBaseMenu.GetInstance().GetSelectedOption(),
            MenuType.Settings => SettingsBaseMenu.GetInstance().GetSelectedOption(),
            MenuType.Token    => TokenBaseMenu.GetInstance().GetSelectedOption(),
            _                 => 0
        };
    }

    private enum MenuType
    {
        Main,
        Settings,
        Token
    }
}