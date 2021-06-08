using System.Threading.Tasks;
using OpenTriviaAPICaller.UI;

namespace OpenTriviaAPICaller
{
    internal static class Program
    {
        public static async Task Main()
        {
            await Task.Run(Menu.Render);
        }
    }
}