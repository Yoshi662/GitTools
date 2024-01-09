using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.OperationsAllRepos
{
    public class PullAllCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            List<Task> tasks = [];
            foreach (var item in Manager.RepositoryList.Where(r => r.IsClean))
            {
                Task task = new(async () =>
                {
                    await GitOperations.PullRepositoryAsync(item.LocalPath);
                });
                tasks.Add(task);
                task.Start();
            }

            AnsiConsole.Status().Start("Pulling All Repos... Please wait", ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                Task.WhenAll(tasks).Wait();
            });
            return true;
        }
    }
}
