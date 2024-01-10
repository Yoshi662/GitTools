using GitTools.Commands.OperationsAllRepos;
using GitTools.Commands.OperationsSingleRepo;
using GitTools.Entities;
using GitTools.Menus;
using GitTools.Utils;

using Spectre.Console;

namespace GitTools.Screens
{
    public class OperationsSingleRepoScreen : IScreen
    {
        private string _selectedRepo = "";

        private List<MenuOption> _options = [
           new MenuOption("Get Status", new GetRepoStatusCommand()),
            ];

        private List<string> _choices = [
            "[darkblue]Get Status[/]",
            "[darkblue]Fetch[/]",
            "[darkblue]Pull[/]",
            "[darkblue]Push[/]",
            "[darkblue]Clean[/]",
            "[darkblue]Add all files and Commit[/]",
            "[darkblue]Open in Terminal[/]",
            "[darkblue]Open in Explorer[/]",
            "[darkblue]Change Repository[/]",
            "[red3]Back[/]"
];

        private GitRepositoryManager _manager = GitRepositoryManager.Instance;

        public void Show()
        {
            if (String.IsNullOrWhiteSpace(_selectedRepo))
                SelectRepo();

            SelectRepoOperations();
        }

        private void SelectRepoOperations()
        {
            Menu menu = new(_options);
            menu.Config.Title = MenuUtils.GetTitle;
            menu.AskAndSelect();
        }

        private void SelectRepo()
        {
            Style cleanStyle = new(Color.Yellow4);
            Style dirtyStyle = new(Color.Orange4);

            List<MenuOption> options = _manager.RepositoryList
                .Select(r => 
                new MenuOption(r.LocalPath){
                    OptionStyle = r.IsClean ? cleanStyle : dirtyStyle
                    }
                )
                .ToList();
            _selectedRepo = new Menu(options).Ask();

        }

        private void UpdateRepoCommands()
        {
            _options.ForEach(o => (o.Command as BaseSingleRepoCommand).SelectedRepo = _selectedRepo);
        }
    }
}
