using System.Threading.Tasks;

namespace OpenTrivia
{
	class Program
	{
		public static async Task Main()
		{
			await Task.Run(() => Menu.Render());
		}
	}
}
