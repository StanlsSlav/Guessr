using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Guessr.Models;
using Guessr.UserSettings;
using static System.Net.WebUtility;
using static System.Text.Json.JsonSerializer;
using static Guessr.Parsing.ErrorsDictionary;

namespace Guessr.Parsing;

internal static class ParseRequest
{
    private const string BaseUrl = "https://opentdb.com/api.php";
    private static HttpClient HttpClient = new();

    public static Trivia Quiz;

    public static async Task ParseQuestionAsync()
    {
        var webResponse = await HttpClient.GetStringAsync(BaseUrl + FilterRequests.Options);
        var root = Deserialize<Root>(webResponse);

        // Had returned an error?
        if (root is null || !root.Results.Any())
        {
            return;
        }

        await HandleResponseCodeAsync(root.ResponseCode);
        Quiz = root.Results.First();

        // Html Decoding
        Quiz.CorrectAnswer = HtmlDecode(Quiz.CorrectAnswer);
        Quiz.Question = HtmlDecode(Quiz.Question);

        for (var i = 0; i < Quiz.IncorrectAnswers.Count; i++)
        {
            Quiz.IncorrectAnswers[i] = HtmlDecode(Quiz.IncorrectAnswers[i]);
        }
    }

    public static List<TriviaCategory> GetCategories()
    {
        var response = HttpClient.GetStringAsync("https://opentdb.com/api_category.php").Result;
        return Deserialize<TriviaCategories>(response).JsonTriviaCategories;
    }

    public static List<TriviaDifficulty> GetDifficulties()
    {
        return Enum.GetValues<TriviaDifficulty>().ToList();
    }

    public static List<TriviaResponseType> GetTypeChoices()
    {
        return Enum.GetValues<TriviaResponseType>().ToList();
    }
}