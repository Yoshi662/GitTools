using GitTools.Commands;
using GitTools.Menus;
using GitTools.Utils;
using Spectre.Console;

namespace GitTools.Screens
{
    public class MainScreen : IScreen
    {
        private List<MenuOption> _options = [
            new MenuOption("Repository Management", new OpenScreenCommand(new RepositoryManagementScreen())),
            new MenuOption("Operations for all Repositories", new OpenScreenCommand(new OperationsAllReposScreen())),
            new MenuOption("Operations for a Single Repository", new OpenScreenCommand(new OperationsSingleRepoScreen()))
            ];

        public MainScreen()
        {
            if (!Git.GitOperations.Exists())
            {
                AnsiConsole.MarkupLine("[red]Git is not installed\nProgram will not work[/]\n[underline]Press any key to continue[/]");
                _options.Clear();
                Console.ReadKey();
                return;
            }
        }

        public void Show()
        {
            Menu menu = new(_options);
            menu.Config.Title = MenuUtils.GetRepoInfoTitle;
            menu.Config.ShowMenuAgainOnCompletedCommand = false;
            if(menu.AskAndSelect() != "Exit")
            {
                Show();
            }
        }
    }
}
