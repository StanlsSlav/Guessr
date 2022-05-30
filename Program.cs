using System.Threading.Tasks;
using Guessr.UI;

namespace Guessr;

internal static class Program
{
    public static async Task Main()
    {
        await Task.Run(Menu.Render);
    }
}