using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenTriviaAPICaller.src.DataModels
{
    public class Root
    {
        [JsonPropertyName("response_code")] public int ResponseCode { get; set; }

        [JsonPropertyName("results")] public List<Trivia> Results { get; set; }
    }

    public class Trivia
    {
        [JsonPropertyName("question")] public string Question { get; set; }

        [JsonPropertyName("category")] public string Category { get; set; }

        [JsonPropertyName("difficulty")] public string Difficulty { get; set; }

        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("correct_answer")] public string CorrectAnswer { get; set; }

        [JsonPropertyName("incorrect_answers")]
        public List<string> IncorrectAnswers { get; set; }
    }
}