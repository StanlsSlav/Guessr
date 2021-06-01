using System.Threading.Tasks;
using OpenTriviaAPICaller.src.UI;

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