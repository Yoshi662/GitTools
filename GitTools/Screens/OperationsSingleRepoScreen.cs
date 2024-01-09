using GitTools.Entities;

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
            throw new NotImplementedException();
        }
    }
}
