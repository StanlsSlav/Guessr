using System;
using System.Threading.Tasks;
using Guessr.UI;
using static Guessr.Parsing.ParseRequest;

namespace Guessr.Parsing;

internal static class ProcessInput
{
    /// <summary>
    ///     Checks if the user input is equal to the correct answer
    ///     Or if a custom command was inputted
    /// </summary>
    public static async Task<bool> CheckIfCorrect(string input)
    {
        // Custom console commands during the game
        switch (input)
        {
            case "Quit":
                Menu.Exit(0);
                break;
            case "Back":
                await Task.Run(Menu.HandleMenuInputAsync);
                break;
        }

        return Quiz.CorrectAnswer.Equals(input);
    }
}