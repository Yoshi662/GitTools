using GitTools.Entities;
using Spectre.Console;

namespace GitTools.Utils
{
    public static class MenuUtils
    {
        private static GitRepositoryManager _manager = GitRepositoryManager.Instance;

        public static string GetTitle => "[bold underline green]Please select an Option[/]" +
                    "\n[dim italic grey]Use the arrow keys and Enter to Select[/]";

        public static string GetRepoInfoTitle => "[bold underline green]Please select an Option[/]" +
                    $"\n\n[{CleanStyle.Foreground}]Tracked Repositories: [/]{_manager.RepositoryList.Count}" +
                    $"\n[{DirtyStyle.Foreground}]Dirty Repositories: [/]{_manager.RepositoryList.Where(r => !r.IsClean).Count()}" +
                    "\n[dim italic grey]Use the arrow keys and Enter to Select[/]";

        public static Style CleanStyle = new(Color.GreenYellow);
        public static Style DirtyStyle = new(Color.Orange4);
    }
}
