using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spectre.Console;
using static Guessr.Parsing.ParseRequest;
using static Guessr.Parsing.ProcessInput;

namespace Guessr;

internal static class StartTrivia
{
    private static readonly Rule _questionRule = new()
    {
        Alignment = Justify.Left,
        Style = new Style(Color.Aquamarine1)
    };

    /// <summary>
    ///     Game UI
    /// </summary>
    public static async Task Start()
    {
        await ParseQuestionAsync();
        AnsiConsole.Clear();

        var answers = new List<string>(4) { Quiz.CorrectAnswer };
        answers.AddRange(Quiz.IncorrectAnswers);
        answers.Sort();

        answers.Add("Back");
        answers.Add("Quit");

        _questionRule.Title = $"[honeydew2]{Quiz.Question.RemoveMarkup()}[/]"; 
        AnsiConsole.Write(_questionRule);
        AnsiConsole.WriteLine();

        var userInput = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .HighlightStyle(new Style(Color.Green))
                .PageSize(4)
                .AddChoices(answers)
        );

        if (await CheckIfCorrect(userInput))
        {
            AnsiConsole.MarkupLine("[green][[+]][/] Correct!");
        }
        else
        {
            AnsiConsole.MarkupLine("[red][[-]][/] Incorrect!");
        }

        await Task.Delay(TimeSpan.FromSeconds(1.5));
        await Start();
    }
}