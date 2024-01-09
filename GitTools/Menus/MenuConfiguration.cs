using Spectre.Console;

namespace GitTools.Menus
{
    public record struct MenuConfiguration(string Title, bool HasBack, Style SelectedStyle, bool ShowMenuAgainOnCompletedCommand);
}
