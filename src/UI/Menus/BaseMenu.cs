using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Guessr.UI.Menus;

/// <summary>
///     Represents a generic base menu
/// </summary>
public abstract class BaseMenu<T> where T : new()
{
    private static T _instance;

    /// <summary>
    ///     Provide static access to non-static parameters
    /// <seealso cref="Header"/>
    /// <seealso cref="Options"/>
    /// </summary>
    /// <returns>The instance of <see cref="T"/></returns>
    public static T GetInstance() => _instance ??= new T();

    /// <summary>
    ///     The menu's header to print
    /// </summary>
    /// <exception cref="Exception">Throws when trying to set the value to empty or null</exception>
    [Required]
    protected abstract string Header { get; }

    /// <summary>
    ///     The menu's options to print
    /// </summary>
    /// <exception cref="Exception">Throws when trying to set the value to an empty list</exception>
    protected abstract IReadOnlyList<string> Options { get; }

    /// <summary>
    ///     Print the menu to the console
    /// </summary>
    public void Render()
    {
        Console.WriteLine(Header + "\n\n");

        for (var i = 0; i < Options.Count; i++)
        {
            ColorFeedBack.Colored(Options[i], prefixToColor: i + 1 + ")");
        }
    }
}