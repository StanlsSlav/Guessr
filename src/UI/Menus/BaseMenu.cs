using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Guessr.UI.Menus;

/// <summary>
///     Represents a generic base menu
/// </summary>
public abstract class BaseMenu<T> where T : new()
{
    private static readonly Lazy<T> _lazy = new(() => new T());

    /// <summary>
    ///     Provide static access to non-static parameters
    /// <seealso cref="Header"/>
    /// <seealso cref="Options"/>
    /// </summary>
    /// <returns>The instance of <see cref="T"/></returns>
    public static T GetInstance() => _lazy.Value;

    private readonly SelectionPrompt<string> _selectionPrompt;

    protected BaseMenu()
    {
        _selectionPrompt = new SelectionPrompt<string>();
        _selectionPrompt.AddChoices(Options);
        _headerRule.Title = Header;
    }

    /// <summary>
    ///     The menu's header to print
    /// </summary>
    /// <exception cref="Exception">Throws when trying to set the value to empty or null</exception>
    protected abstract string Header { get; }

    /// <summary>
    ///     The menu's options to print
    /// </summary>
    /// <exception cref="Exception">Throws when trying to set the value to an empty list</exception>
    protected abstract List<string> Options { get; }

    private readonly Rule _headerRule = new()
    {
        Alignment = Justify.Left,
        Style = new Style(Color.Aquamarine1)
    };

    public int GetSelectedOption()
    {
        AnsiConsole.Write(_headerRule);
        AnsiConsole.WriteLine();

        return Options.IndexOf(AnsiConsole.Prompt(
                   _selectionPrompt
               )) + 1;
    }
}