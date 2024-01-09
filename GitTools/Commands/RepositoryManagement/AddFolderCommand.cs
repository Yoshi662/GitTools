using GitTools.Entities;
using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.RepositoryManagement
{
    public class AddFolderCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            string path = AnsiConsole.Ask<string>("Please enter the Path of the Folder: ");
            List<string> repos = new List<string>();
            List<Task> tasks = [];

            if (!Directory.Exists(path))
            {
                AnsiConsole.MarkupLine("[red]The path is not a valid folder[/]\n\n");
                return false;
            }

            AnsiConsole.Status().Start("Getting all the folders inside... Please wait", ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                RecursiveSearch(path, ref repos);
            });

            repos = repos
                .Distinct()
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToList();

            foreach (string repo in repos)
            {
                Task task = new Task(async () =>
                {
                    bool isDirty = await GitOperations.IsRepoCleanAsync(repo);
                    Manager.RepositoryList.Add(new GitRepository(repo, isDirty));
                });
                tasks.Add(task);
                task.Start();
            }

            AnsiConsole.Status().Start("Adding all the repos... Please wait", ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                Task.WhenAll(tasks).Wait();
            });
            Manager.Save();
            return true;
        }

        private void RecursiveSearch(string path, ref List<string> repos)
        {
            string[] folders = Directory.GetDirectories(path);
            if (folders.Contains(".git") || GitOperations.IsPathARepoAsync(path).Result)
            {
                repos.Add(path);
                return;
            }
            else
            {
                foreach (string folder in folders)
                {
                    RecursiveSearch(folder, ref repos);
                }
            }
        }
    }
}
