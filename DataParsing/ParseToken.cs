using System;
using System.IO;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace OpenTriviaAPICaller
{
	class ParseAPIToken
	{
		private static readonly string _BaseUrl = "https://opentdb.com/api_token.php?command=";
		private static readonly System.Net.Http.HttpClient _Client = new();

		public static APIToken ParsedToken = new();

		public static async Task RetrieveToken()
		{
			var webContent = await _Client.GetStringAsync(_BaseUrl + "request");
			ParsedToken = DeserializeObject<APIToken>(webContent);

			UpdateTime();
			await File.WriteAllTextAsync("token.json", SerializeObject(ParsedToken));
		}

		public static async Task ResetToken()
		{
			var webContent = await _Client.GetStringAsync(_BaseUrl + "reset&token=" + ParsedToken.Token);

			UpdateTime();
			await ErrorsDictionary.HandleResponseCode(DeserializeObject<APIToken>(webContent).ResponseCode);
		}

		public static void LoadToken()
		{
			if (File.Exists("token.json"))
				ParsedToken = DeserializeObject<APIToken>(File.ReadAllText("token.json"));
		}

		private static void UpdateTime() => ParsedToken.RequestDate = DateTime.Now;
	}
}
