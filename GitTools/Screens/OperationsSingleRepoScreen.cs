
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
        private GitRepository _selectedRepo = null;

        private List<MenuOption> _options = [
            new("Get Status", new GetRepoStatusCommand()),
            new("Fetch" , new FetchCommand()),
            new("Pull" , new PullCommand()),
            new("Push" , new PushCommand()),
            new("Clean" , new CleanCommand()),
            new("Add all files and Commit" , new AddAllAndCommitCommand()),
            new("Open in Terminal" , new OpenInTerminalCommand()),
            new("Open in Explorer" , new OpenInExplorerCommand()),
            new("Checkout Branch", new CheckoutBranchCommand()),
            new("List Branch", new ListBranchCommand()),
            new("Change Repository", new ChangeRepositoryCommand()) //this must always be the last option
            ];


        private GitRepositoryManager _manager = GitRepositoryManager.Instance;

        public void Show()
        {
            SelectRepo();
            SelectRepoOperations();
        }

        private void SelectRepoOperations()
        {
            Menu menu = new(_options);
            menu.Config.Title = MenuUtils.GetTitle;
            AnsiConsole.MarkupLine($"Selected Repo: [{(_selectedRepo.IsClean ? MenuUtils.CleanStyle.ToMarkup() : MenuUtils.DirtyStyle.ToMarkup())}]{_selectedRepo.LocalPath}[/]\n");
            string option = menu.Ask(false);
            if (option == _options.Last().MarkupOptionName)
            {
                SelectRepo();
                SelectRepoOperations();
            }
            else
            {
                menu.Select(option);
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
            var styledanswer = new Menu(
                options, new MenuConfiguration()
                {
                    HasBack = false,     
                })
                .Ask();        
            _selectedRepo = _manager.RepositoryList.FirstOrDefault(r => r.LocalPath == options.Find(o => o.MarkupOptionName == styledanswer)?.OptionName);
            
            UpdateRepoCommands();
        }

        private void UpdateRepoCommands()
        {
            _options.ForEach(o => (o.Command as BaseSingleRepoCommand).SelectedRepo = _selectedRepo?.LocalPath);
        }
    }
}
