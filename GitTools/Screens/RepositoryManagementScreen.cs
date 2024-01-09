using GitTools.Commands.RepositoryManagement;
using GitTools.Entities;
using GitTools.Menus;
using GitTools.Utils;

namespace GitTools.Screens
{
    class RepositoryManagementScreen : IScreen
    {
        private List<MenuOption> _options = [
            new MenuOption("Lists Repositories", new ListReposCommand()),
            new MenuOption("Add a Repository", new AddRepositoryCommand()),
            new MenuOption("Scans and adds a folder", new AddFolderCommand()),
            new MenuOption("Remove a Repository", new RemoveRepositoryCommand()),
            new MenuOption("REMOVE ALL REPOSITORIES", new RemoveAllReposCommand()),
            ];

        private GitRepositoryManager _manager = GitRepositoryManager.Instance;

        public void Show()
        {
            Menu menu = new(_options);
            menu.Config.Title = MenuUtils.GetRepoInfoTitle;
            menu.ShowAndSelect();
        }
    }
}

