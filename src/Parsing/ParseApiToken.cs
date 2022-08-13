using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Guessr.Models;
using static System.Text.Json.JsonSerializer;

namespace Guessr.Parsing;

internal static class ParseApiToken
{
    private const string BaseUrl = "https://opentdb.com/api_token.php?command=";
    private static readonly HttpClient Client = new();

    private static readonly FileInfo _tokenFile = new("token.json");
    public static ApiToken ParsedApiToken = new();

    public static async Task RetrieveToken()
    {
        var webContent = await Client.GetStringAsync(BaseUrl + "request");
        ParsedApiToken = Deserialize<ApiToken>(webContent);

        await using var stream = _tokenFile.OpenWrite();
        var buffer = Encoding.ASCII.GetBytes(
            Serialize(ParsedApiToken, new JsonSerializerOptions { WriteIndented = true }));

        UpdateTokenTime(wasTokenReset: true);
        await stream.WriteAsync(buffer);
    }

    public static async Task ResetToken()
    {
        var webContent = await Client.GetStringAsync($"{BaseUrl}reset&token={ParsedApiToken.Token}");

        await ErrorsDictionary.HandleResponseCodeAsync(Deserialize<ApiToken>(webContent)!.ResponseCode);
        UpdateTokenTime(wasTokenReset: true);
    }

    public static async Task LoadTokenAsync()
    {
        if (!_tokenFile.Exists)
        {
            return;
        }

        await using var stream = _tokenFile.OpenRead();
        var buffer = new byte[_tokenFile.Length];
        _ = await stream.ReadAsync(buffer);

        ParsedApiToken = Deserialize<ApiToken>(Encoding.ASCII.GetString(buffer));
    }

    private static void UpdateTokenTime(bool wasTokenReset = false)
    {
        if (wasTokenReset)
        {
            ParsedApiToken.RequestDate = DateTime.Now;
            ParsedApiToken.ExpirationDate = ParsedApiToken.RequestDate.AddHours(6);
        }

        ParsedApiToken.LeftTime = ParsedApiToken.ExpirationDate - DateTime.Now;
    }

    public static bool DoesTokenExist()
    {
        UpdateTokenTime();
        return _tokenFile.Exists;
    }

    public static bool IsTokenValid()
    {
        return DoesTokenExist() && ParsedApiToken.LeftTime.TotalSeconds > 0;
    }
}