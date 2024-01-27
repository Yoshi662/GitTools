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
            List<string> repos = [];
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
                repos.AddRange(Search(path));
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

        private List<string> Search(string path)
        {
            List<string> repos = [];
            List<Task> tasks = []; 
            string[] folders = Directory.GetDirectories(path, "*.git", new EnumerationOptions()
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = true,
                AttributesToSkip = FileAttributes.None
            });
            foreach (var folder in folders)
            {
                Task task = new(() =>
                {
                    var sanitiedFolder = folder.Replace(".git", "");

                    if (GitOperations.IsPathARepoAsync(sanitiedFolder).Result)
                        repos.Add(sanitiedFolder);
                });
                tasks.Add(task);
                task.Start();
            }
            Task.WhenAll(tasks).Wait();
            return repos;
        }
    }
}
