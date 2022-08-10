using System.Collections.Generic;

namespace Guessr.UI.Menus;

public class SettingsBaseMenu : BaseMenu<SettingsBaseMenu>
{
    protected override string Header => "Settings Menu";
    protected override IReadOnlyList<string> Options { get; } = new[] { "Category", "Difficulty", "Type", "Back" };
}