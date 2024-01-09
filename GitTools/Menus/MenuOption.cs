using GitTools.Commands;
using Spectre.Console;

namespace GitTools.Menus
{
    public class MenuOption
    {
        public MenuOption(string optionName, ICommand command)
        {
            OptionName = optionName;
            Command = command;
        }

        public string OptionName { get; init; }
        public ICommand Command { get; init; }
        public Style OptionStyle { get; init; } = new(Color.Blue);
        public string MarkupOptionName => $"[{OptionStyle.ToMarkup()}]{OptionName}[/]";
    }
}
