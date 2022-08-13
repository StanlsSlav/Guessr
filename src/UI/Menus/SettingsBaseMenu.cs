using System.Collections.Generic;

namespace Guessr.UI.Menus;

public class SettingsBaseMenu : BaseMenu<SettingsBaseMenu>
{
    protected override string Header => "Settings";
    protected override List<string> Options { get; } = new() { "Category", "Difficulty", "Type", "Back" };
}