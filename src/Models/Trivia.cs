using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Guessr.Models;

[StructLayout(LayoutKind.Auto)]
public class Root
{
    [JsonPropertyName("response_code")]
    public int ResponseCode { get; set; }

    [JsonPropertyName("results")]
    public List<Trivia> Results { get; set; }
}

[StructLayout(LayoutKind.Auto)]
public class Trivia
{
    [JsonPropertyName("question")]
    public string Question { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("difficulty")]
    public string Difficulty { set; get; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("correct_answer")]
    public string CorrectAnswer { get; set; }

    [JsonPropertyName("incorrect_answers")]
    public List<string> IncorrectAnswers { get; set; }
}