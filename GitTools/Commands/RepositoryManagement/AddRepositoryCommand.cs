using GitTools.Entities;
using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.RepositoryManagement
{
    public class AddRepositoryCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            string path = AnsiConsole.Ask<string>("Please enter the path of the Repository: ");
            if (!Directory.Exists(path))
            {
                AnsiConsole.MarkupLine("[red]The path is not a valid folder[/]\n\n");
                return false;
            }
            bool isRepo = GitOperations.IsPathARepoAsync(path).Result;
            if (!isRepo)
            {
                AnsiConsole.MarkupLine("[red]The path is not a Git Repository[/]\n\n");
                return false;
            }

            bool isClean = GitOperations.IsRepoCleanAsync(path).Result;
            Manager.RepositoryList.Add(new GitRepository(path, isClean));
            Manager.Save();
            return true;
        }
    }
}
