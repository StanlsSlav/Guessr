using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OpenTriviaAPICaller.src.DataModels;
using OpenTriviaAPICaller.src.DataParsing.HandleAPIErrors;
using static System.Text.Json.JsonSerializer;

namespace OpenTriviaAPICaller.src.DataParsing
{
    internal static class ParseApiToken
    {
        private const string BaseUrl = "https://opentdb.com/api_token.php?command=";
        private static readonly HttpClient Client = new();

        public static ApiToken ParsedToken = new();

        public static async Task RetrieveToken()
        {
            var webContent = await Client.GetStringAsync(BaseUrl + "request");
            ParsedToken = Deserialize<ApiToken>(webContent);

            UpdateTime();
            await File.WriteAllTextAsync("token.json",
                Serialize(ParsedToken, new JsonSerializerOptions {WriteIndented = true}));
        }

        public static async Task ResetToken()
        {
            var webContent = await Client.GetStringAsync(BaseUrl + "reset&token=" + ParsedToken.Token);

            UpdateTime();
            await ErrorsDictionary.HandleResponseCode(Deserialize<ApiToken>(webContent)!.ResponseCode);
        }

        public static void LoadToken()
        {
            if (File.Exists("token.json"))
                ParsedToken = Deserialize<ApiToken>(File.ReadAllText("token.json"));
        }

        private static void UpdateTime()
        {
            ParsedToken.RequestDate = DateTime.Now;
        }
    }
}