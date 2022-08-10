using System.Collections.Generic;
using static Guessr.ColorFeedBack;

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

        Colored($"{typeof(T).Name} set to {GetCurrent()}");
    }

    public T GetCurrent()
    {
        return _repeatable[_index];
    }
}