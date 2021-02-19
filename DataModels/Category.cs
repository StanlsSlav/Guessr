/* ============================================
 *					Yet to use
 * ============================================ */


using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenTriviaAPICaller
{
	class TriviaCategories
	{
		[JsonProperty("trivia_categories")]
		public List<Category> Categories { get; set; }
	}

	class Category
	{
		[JsonProperty("id")]
		public int ID { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
