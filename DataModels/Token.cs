using Newtonsoft.Json;

namespace OpenTrivia
{
    public class APIToken
    {
        [JsonProperty("response_code")]
        public int ResponseCode { get; set; }

        [JsonProperty("response_message")]
        public string ResponseMessage { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        public System.DateTime RequestDate { get; set; }
    }
}
