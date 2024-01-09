using GitTools.Entities;

namespace GitTools.Utils
{
    public static class MenuUtils
    {
        private static GitRepositoryManager _manager = GitRepositoryManager.Instance;

        public static string GetTitle => "[bold underline green]Please select an Option[/]" +
                    "\n[dim italic grey]Use the arrow keys and Enter to Select[/]";

        public static string GetRepoInfoTitle => "[bold underline green]Please select an Option[/]" +
                    $"\n\n[yellow4]Tracked Repositories: [/]{_manager.RepositoryList.Count}" +
                    $"\n[orange4]Dirty Repositories: [/]{_manager.RepositoryList.Where(r => !r.IsClean).Count()}" +
                    "\n[dim italic grey]Use the arrow keys and Enter to Select[/]";
    }
}
