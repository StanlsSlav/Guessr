using System.Collections.Generic;

namespace Guessr.UI.Menus;

public class TokenBaseMenu : BaseMenu<TokenBaseMenu>
{
    protected override string Header => "Token Handling";

    protected override List<string> Options { get; } =
        new() { "Retrieve", "Reset", "Status", "Open Explorer", "Back" };
}