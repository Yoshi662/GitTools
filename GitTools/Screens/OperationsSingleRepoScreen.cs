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
            new MenuOption("Fetch" , new FetchCommand()),
            new MenuOption("Pull" , new PullCommand()),
            new MenuOption("Push" , new PushCommand()),
            new MenuOption("Clean" , new CleanCommand()),
            new MenuOption("Add all files and Commit" , new AddAllAndCommitCommand()),
            new MenuOption("Open in Terminal" , new OpenInTerminalCommand()),
            new MenuOption("Open in Explorer" , new OpenInExplorerCommand()),
            new MenuOption("Change Repository", new ChangeRepositoryCommand())
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
            if (menu.AskAndSelect() == "Change Repository")
            {
                _selectedRepo = "";
                menu.Config.ShowMenuAgainOnCompletedCommand = false;
                Show();
            }
               
        }

        private void SelectRepo()
        {
            List<MenuOption> options = _manager.RepositoryList
                .Select(r => 
                new MenuOption(r.LocalPath){
                    OptionStyle = r.IsClean ? MenuUtils.CleanStyle : MenuUtils.DirtyStyle
                    }
                )
                .ToList();
            var styledanswer = new Menu(options).Ask();
            _selectedRepo = options.Find(o => o.MarkupOptionName == styledanswer).OptionName;
            
            UpdateRepoCommands();
        }

        private void UpdateRepoCommands()
        {
            _options.ForEach(o => (o.Command as BaseSingleRepoCommand).SelectedRepo = _selectedRepo);
        }
    }
}
