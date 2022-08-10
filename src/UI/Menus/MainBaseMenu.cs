using System.Collections.Generic;

namespace Guessr.UI.Menus;

public class MainBaseMenu : BaseMenu<MainBaseMenu>
{
    protected override string Header => "Welcome to Guessr!\nA trivia game based on OpenTrivia API";
    protected override IReadOnlyList<string> Options { get; } = new[] { "Start", "Settings", "Token", "Exit" };
}