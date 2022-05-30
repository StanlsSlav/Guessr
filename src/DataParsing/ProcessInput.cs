﻿using System;
using System.Threading.Tasks;
using OpenTriviaAPICaller.UI;
using static OpenTriviaAPICaller.DataParsing.ParseRequest;

namespace OpenTriviaAPICaller.DataParsing;

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
            case "quit" or "q":
                Menu.Exit(0);
                break;
            case "back" or "b":
                await Task.Run(Menu.Render);
                break;
        }

        // CapsLock forgotten
        return string.Equals(input, Quiz.CorrectAnswer, StringComparison.CurrentCultureIgnoreCase);
    }
}