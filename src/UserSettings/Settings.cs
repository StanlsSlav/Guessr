using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Guessr.Models;
using static Guessr.Parsing.ParseRequest;

namespace Guessr.UserSettings;

internal static class Settings
{
    public static Categories Category => Categories.Instance;

    public static Difficulties Difficulty => Difficulties.Instance;

    public static ChoiceTypes ChoiceType => ChoiceTypes.Instance;
}

[StructLayout(LayoutKind.Auto)]
internal class ChoiceTypes : Repeatable<TriviaResponseType>
{
    private static readonly Lazy<ChoiceTypes> _lazy = new(() => new ChoiceTypes());
    public static readonly ChoiceTypes Instance = _lazy.Value;

    public ChoiceTypes() : base(GetTypeChoices()) { }

    protected override List<TriviaResponseType> GetItems()
    {
        return GetTypeChoices();
    }
}

[StructLayout(LayoutKind.Auto)]
internal class Categories : Repeatable<TriviaCategory>
{
    private static readonly Lazy<Categories> _lazy = new(() => new Categories());
    public static readonly Categories Instance = _lazy.Value;

    public Categories() : base(GetCategories())
    {
        _repeatable.Insert(0, new TriviaCategory
        {
            Name = "Any"
        });
    }

    protected override List<TriviaCategory> GetItems()
    {
        return GetCategories();
    }
}

[StructLayout(LayoutKind.Auto)]
internal class Difficulties : Repeatable<TriviaDifficulty>
{
    private static readonly Lazy<Difficulties> _lazy = new(() => new Difficulties());
    public static readonly Difficulties Instance = _lazy.Value;

    public Difficulties() : base(GetDifficulties()) { }

    protected override List<TriviaDifficulty> GetItems()
    {
        return GetDifficulties();
    }
}