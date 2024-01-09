using Spectre.Console;

namespace GitTools.Commands.RepositoryManagement
{
    public class RemoveAllReposCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            bool confirm = AnsiConsole.Confirm("[blink bold underline white on red]ARE YOU SURE?[/]", false);
            if (!confirm)
            {
                return false;
            }
            Manager.RepositoryList.Clear();
            Manager.Save();
            return true;
        }
    }
}
