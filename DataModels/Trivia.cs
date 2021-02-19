using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenTriviaAPICaller
{
    public class Root
    {
        [JsonProperty("response_code")]
        public int ResponseCode { get; set; }

        [JsonProperty("results")]
        public List<Trivia> Results { get; set; }
    }

    public class Trivia
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; }

        [JsonProperty("incorrect_answers")]
        public List<string> IncorrectAnswers { get; set; }
    }
}
