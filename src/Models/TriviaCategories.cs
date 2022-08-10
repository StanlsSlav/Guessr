using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Guessr.Models;

/// <summary>
///     <see cref="TriviaCategory" /> alone can't handle the deserialization.
///     This struct is required in order to successfully deserialize the api call.
/// </summary>
public record struct TriviaCategories
{
    [JsonPropertyName("trivia_categories")]
    public List<TriviaCategory> JsonTriviaCategories { get; set; }
}

[StructLayout(LayoutKind.Auto)]
public record struct TriviaCategory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}