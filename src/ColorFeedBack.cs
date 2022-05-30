using System;

namespace OpenTriviaAPICaller;

internal static class ColorFeedBack
{
    /// <summary>
    ///     Give some color to the console output
    /// </summary>
    /// <param name="input">Required, shows the text after the colored prefix</param>
    /// <param name="prefixToColor">Optional, the prefix before the input</param>
    /// <param name="foreground">Optional, the color the prefix should be</param>
    public static void Colored(string input,
        string prefixToColor = "[+] ",
        ConsoleColor foreground = ConsoleColor.Blue)
    {
        Console.ForegroundColor = foreground;
        Console.Write(prefixToColor + ' ');

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(input);
    }
}