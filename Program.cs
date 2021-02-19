using System.Threading.Tasks;

namespace OpenTriviaAPICaller
{
	class Program
	{
		public static async Task Main()
		{
			/* Discover the right way of changing categories
			 * Uncomment once you get the categories work without the Enum
			 *
			 * await ParseCategory.InitializeCategories(); */
			await Task.Run(() => Menu.Render());
		}
	}
}
