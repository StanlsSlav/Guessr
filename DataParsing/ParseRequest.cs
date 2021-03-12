using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.WebUtility;
using static System.Text.Json.JsonSerializer;
using static OpenTrivia.ErrorsDictionary;

namespace OpenTrivia
{
	class ParseRequest
	{
		private static readonly string _BaseUrl = "https://opentdb.com/api.php";

		public static Trivia Quizz;
		public static async Task ParseQuestion()
		{
			var webResponse = await new HttpClient().GetStringAsync(_BaseUrl + FilterRequests.Options);
			var root = Deserialize<Root>(webResponse);

			//Had returned an error?
			await HandleResponseCode(root.ResponseCode);

			//The API will always respond with 1 trivia object
			Quizz = root.Results[0];

			#region Bad Practice
			//Html Decoding
			Quizz.CorrectAnswer = HtmlDecode(Quizz.CorrectAnswer);
			Quizz.Question = HtmlDecode(Quizz.Question);

			for (int i = 0; i < Quizz.IncorrectAnswers.Count; i++)
				Quizz.IncorrectAnswers[i] = HtmlDecode(Quizz.IncorrectAnswers[i]);
			#endregion
		}
	}
}
