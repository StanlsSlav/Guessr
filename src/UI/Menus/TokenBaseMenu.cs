using System.Collections.Generic;

namespace Guessr.UI.Menus;

public class TokenBaseMenu : BaseMenu<TokenBaseMenu>
{
    protected override string Header => "Token Handling Menu";

    protected override IReadOnlyList<string> Options { get; } =
        new[] { "Retrieve", "Reset", "Status", "Open Explorer", "Back" };
}