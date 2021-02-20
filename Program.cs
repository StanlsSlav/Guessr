using System.Threading.Tasks;

namespace OpenTriviaAPICaller
{
	class Program
	{
		public static async Task Main()
		{
			await Task.Run(() => Menu.Render());
		}
	}
}
