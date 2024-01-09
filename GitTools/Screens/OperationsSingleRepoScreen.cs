using GitTools.Entities;
using GitTools.Menus;

using Spectre.Console;

namespace GitTools.Screens
{
    public class OperationsSingleRepoScreen : IScreen
    {
        private string _selectedRepo = "";

        private List<string> _choices = [
            "[darkblue]Get Status[/]",
            "[darkblue]Fetch[/]",
            "[darkblue]Pull[/]",
            "[darkblue]Push[/]",
            "[darkblue]Clean[/]",
            "[darkblue]Add all files and Commit[/]",
            "[darkblue]Open in Terminal[/]",
            "[darkblue]Open in Explorer[/]",
            "[red3]Back[/]"
];

        private GitRepositoryManager _manager = GitRepositoryManager.Instance;

        public void Show()
        {

        }

        private void SelectRepoOperations()
        {

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
    }
}
