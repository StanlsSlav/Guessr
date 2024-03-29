﻿using System;
using System.Threading.Tasks;
using static System.ConsoleColor;
using static Guessr.Parsing.ParseRequest;
using static Guessr.Parsing.ProcessInput;
using static Guessr.ColorFeedBack;

namespace Guessr;

internal static class StartTrivia
{
    /// <summary>
    ///     Game UI
    /// </summary>
    public static async Task Start()
    {
        Console.CursorVisible = false;

        while (true)
        {
            await ParseQuestionAsync();

            // Set the place of the right answer as an index
            var placesTillCorrect = Random.Shared.Next(0, Quiz.IncorrectAnswers.Count + 1);

            // Save the correct choice number as an non index
            var correctChoiceNr = placesTillCorrect + 1;

            // Fix the repeating of the last wrong answer
            var toRest = 0;

            Console.WriteLine(Quiz.Question + '\n');
            var showPlaceNr = 1;

            for (var i = 0; i <= Quiz.IncorrectAnswers.Count; i++)
            {
                var isPlaceForCorrect = placesTillCorrect == 0;

                Colored(
                    isPlaceForCorrect ? Quiz.CorrectAnswer.TrimStart() : Quiz.IncorrectAnswers[i - toRest],
                    prefixToColor: showPlaceNr + ")",
                    foreground: DarkYellow
                );

                if (isPlaceForCorrect) toRest++;
                placesTillCorrect--;
                showPlaceNr++;
            }

            Console.Write("\nAnswer: ");
            var userInput = Console.ReadLine();

            // Count as correct if the input is a number and it's equal to the correct answer
            if (int.TryParse(userInput, out var userIntInput) && userIntInput == correctChoiceNr)
            {
                userInput = Quiz.CorrectAnswer;
            }

            if (await CheckIfCorrect(userInput))
            {
                Colored("Correct!", foreground: Green);
            }
            else
            {
                Colored("Incorrect", foreground: Red);
            }

            await Task.Delay(TimeSpan.FromSeconds(1.5));
            Console.Clear();
        }
    }
}