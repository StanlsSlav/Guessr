using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Guessr.Models;
using static System.Text.Json.JsonSerializer;

namespace Guessr.Parsing;

internal static class ParseApiToken
{
    private const string BaseUrl = "https://opentdb.com/api_token.php?command=";
    private static readonly HttpClient Client = new();

    public static ApiToken ParsedApiToken = new();

    public static async Task RetrieveToken()
    {
        var webContent = await Client.GetStringAsync(BaseUrl + "request");
        ParsedApiToken = Deserialize<ApiToken>(webContent);

        UpdateTime();
        await File.WriteAllTextAsync("token.json",
            Serialize(ParsedApiToken, new JsonSerializerOptions { WriteIndented = true }));
    }

    public static async Task ResetToken()
    {
        var webContent = await Client.GetStringAsync(BaseUrl + "reset&token=" + ParsedApiToken.Token);

        UpdateTime();
        await ErrorsDictionary.HandleResponseCodeAsync(Deserialize<ApiToken>(webContent)!.ResponseCode);
    }

    public static void LoadToken()
    {
        if (File.Exists("token.json"))
        {
            ParsedApiToken = Deserialize<ApiToken>(File.ReadAllText("token.json"));
        }
    }

    private static void UpdateTime()
    {
        ParsedApiToken.RequestDate = DateTime.Now;
    }
}