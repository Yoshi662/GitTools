using GitTools.Commands;
using GitTools.Utils;
using Spectre.Console;

namespace GitTools.Menus
{
    public class Menu : IMenu
    {
        List<MenuOption> Options;
        public MenuConfiguration Config;

        public Menu(IEnumerable<MenuOption> options)
        {
            Options = options.ToList();
            Config = new(
                MenuUtils.GetTitle,
                true,
                new(Color.White, null, Decoration.Underline),
                true
                );
            TryAddBack();
        }

        public Menu(IEnumerable<MenuOption> options, MenuConfiguration config)
        {
            Options = options.ToList();
            Config = config;
            TryAddBack();
        }

        private void TryAddBack()
        {
            if (Config.HasBack)
                Options.Add(new ExitOption());
        }

        public void ShowAndSelect()
        {
            string[] choicelist = Options.Select(o => o.MarkupOptionName).ToArray();
            AnsiConsole.Clear();
            string response = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(Config.Title)
                    .AddChoices(choicelist)
                    .HighlightStyle(new Style(Color.White, null, Decoration.Underline))
            );

            MenuOption selectedOption = Options.Find(o => o.MarkupOptionName == response);
            selectedOption.Command.Run();
            if (selectedOption.Command is ExitCommand) return;
            if (Config.ShowMenuAgainOnCompletedCommand) ShowAndSelect();
        }
    }
}
