using GitTools.Commands;
using GitTools.Utils;
using Spectre.Console;

namespace GitTools.Menus
{
    public class Menu : IMenu
    {
        List<MenuOption> Options;
        public MenuConfiguration Config;
        public static MenuConfiguration DefaultMenuConfig = new(
                MenuUtils.GetTitle,
                true,
                new(Color.White, null, Decoration.Underline),
                true
                );

        public Menu(IEnumerable<MenuOption> options)
        {
            Options = options.ToList();
            Config = DefaultMenuConfig;
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

        public string AskAndSelect()
        {
            return Select(Ask());
        }

        public string Ask(bool ClearConsole = true)
        {
            string[] choicelist = Options.Select(o => o.MarkupOptionName).ToArray();
            if(ClearConsole) AnsiConsole.Clear();
            string response = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(Config.Title)
                    .AddChoices(choicelist)
                    .HighlightStyle(new Style(Color.White, null, Decoration.Underline))
            );

            return response;
        }

        public string Select(string option)
        {
            MenuOption selectedOption = Options.Find(o => o.MarkupOptionName == option);
            selectedOption.Command.Run();
            if (selectedOption.Command is ExitCommand) return selectedOption.OptionName;
            return selectedOption.OptionName;
        }
    }
}
