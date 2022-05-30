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

    public static Trivia Quiz;

    public static async Task ParseQuestion()
    {
        var webResponse = await new HttpClient().GetStringAsync(BaseUrl + FilterRequests.Options);
        var root = Deserialize<Root>(webResponse);

        // Had returned an error?
        if (root is not null)
        {
            await HandleResponseCode(root.ResponseCode);

            // The API will always respond with 1 trivia object
            Quiz = root.Results[0];
        }

        // Html Decoding
        Quiz.CorrectAnswer = HtmlDecode(Quiz.CorrectAnswer);
        Quiz.Question = HtmlDecode(Quiz.Question);

        for (var i = 0; i < Quiz.IncorrectAnswers.Count; i++)
            Quiz.IncorrectAnswers[i] = HtmlDecode(Quiz.IncorrectAnswers[i]);
    }
}