using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guessr.UI;
using static System.ConsoleColor;
using static Guessr.ColorFeedBack;
using static Guessr.Parsing.ParseApiToken;

namespace Guessr.Parsing;

internal static class ErrorsDictionary
{
    /// <summary>
    ///     int: The return code from API
    ///     string[0]: What happened
    ///     string[1]: Why it might've happen
    /// </summary>
    private static readonly Dictionary<int, string[]> Error = new()
    {
        [1] = new[]
        {
            "No Results", "Could not return results. The API doesn't have enough questions for your query."
        },
        [2] = new[]
        {
            "Invalid Parameter",
            "Contains an invalid parameter. Arguments passed in aren't valid"
        },
        [3] = new[]
        {
            "Token Not Found",
            "Session Token does not exist"
        },
        [4] = new[]
        {
            "Token Empty",
            "Session Token has returned all possible questions for the specified query. Resetting the Token is necessary"
        }
    };

    public static async Task HandleResponseCodeAsync(int responseCode)
    {
        var isTokenError = responseCode is 3 or 4;

        // Code 0 is an all clear
        if (responseCode == 0)
        {
            return;
        }

    showError:
        Colored(
            input: $"ERROR ENCOUNTERED! Continue? (Y/N)\n\t{Error[responseCode][0]} -- {Error[responseCode][1]}\n",
            foreground: Red
        );

        if (isTokenError)
        {
            Colored(input: "Retrieve a token? (R)", foreground: Yellow);
        }

        // Continue or bail?
        switch (Console.ReadKey(true))
        {
            case { Key: ConsoleKey.Y }:
                Console.Clear();
                break;

            case { Key: ConsoleKey.N }:
                Menu.Exit(0);
                break;

            case { Key: ConsoleKey.R }:
                if (isTokenError)
                    await RetrieveToken();
                break;

            default:
                Console.Clear();
                goto showError;
        }
    }
}