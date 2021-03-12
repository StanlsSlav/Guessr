using System.Text.Json.Serialization;

namespace OpenTrivia
{
    public class APIToken
    {
        [JsonPropertyName("response_code")]
        public int ResponseCode { get; set; }

        [JsonPropertyName("response_message")]
        public string ResponseMessage { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        public System.DateTime RequestDate { get; set; }
    }
}
