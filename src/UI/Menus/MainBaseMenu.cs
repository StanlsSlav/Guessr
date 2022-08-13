using System.Collections.Generic;

namespace Guessr.UI.Menus;

public class MainBaseMenu : BaseMenu<MainBaseMenu>
{
    protected override string Header => "Welcome to Guessr! A trivia game based on OpenTrivia API";
    protected override List<string> Options { get; } = new() { "Start", "Settings", "Token", "Exit" };
}