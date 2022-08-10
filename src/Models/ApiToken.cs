using System;
using System.Text.Json.Serialization;

namespace Guessr.Models;

public class ApiToken
{
    [JsonPropertyName("response_code")]
    public int ResponseCode { get; set; }

    [JsonPropertyName("response_message")]
    public string ResponseMessage { get; set; }

    [JsonPropertyName("token")]
    public string Token { get; set; }

    public DateTime RequestDate { get; set; }
}