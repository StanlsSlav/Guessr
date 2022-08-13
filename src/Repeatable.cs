using System.Collections.Generic;
using Spectre.Console;

namespace Guessr;

public abstract class Repeatable<T>
{
    public List<T> _repeatable;
    private int _index;

    protected abstract List<T> GetItems();

    protected Repeatable(List<T> items)
    {
        _repeatable = items;
    }

    public void Next()
    {
        _index++;
        _index = _index >= _repeatable.Count ? 0 : _index;

        AnsiConsole.MarkupLineInterpolated($"[cyan]{typeof(T).Name}[/] set to [cyan]{GetCurrent()}[/]");
    }

    public T GetCurrent()
    {
        return _repeatable[_index];
    }
}