using GitTools.Commands.OperationsAllRepos;
using GitTools.Entities;
using GitTools.Menus;
using GitTools.Utils;

namespace GitTools.Screens
{
    public class OperationsAllReposScreen : IScreen
    {
        private List<MenuOption> _options = [
            new MenuOption("Lists Repositories", new ListReposCommand()),
            new MenuOption("Fetch All", new FetchAllCommand()),
            new MenuOption("Pull all", new PullAllCommand()),
            new MenuOption("Checkout all to master", new CheckoutToMasterCommand())
            ];

        private GitRepositoryManager _manager = GitRepositoryManager.Instance;

        public void Show()
        {
            Menu menu = new(_options);
            menu.Config.Title = MenuUtils.GetRepoInfoTitle;
            menu.AskAndSelect();
        }
    }
}
