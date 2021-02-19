using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.WebUtility;
using static OpenTriviaAPICaller.ErrorsDictionary;
using static Newtonsoft.Json.JsonConvert;

namespace OpenTriviaAPICaller
{
	class ParseRequest
	{
		private static readonly string _BaseUrl = "https://opentdb.com/api.php";

		public static Trivia _Trivia;
		public static async Task ParseQuestion()
		{
			var webResponse = await new HttpClient().GetStringAsync(_BaseUrl + FilterRequests.Options);
			var root = DeserializeObject<Root>(webResponse);

			//Had returned an error?
			await HandleResponseCode(root.ResponseCode);

			//The API will always respond with 1 trivia object
			_Trivia = root.Results[0];

			#region Bad Practice
			//Html Decoding
			_Trivia.CorrectAnswer = HtmlDecode(_Trivia.CorrectAnswer);
			_Trivia.Question = HtmlDecode(_Trivia.Question);

			for (int i = 0; i < _Trivia.IncorrectAnswers.Count; i++)
				_Trivia.IncorrectAnswers[i] = HtmlDecode(_Trivia.IncorrectAnswers[i]);
			#endregion
		}
	}
}
