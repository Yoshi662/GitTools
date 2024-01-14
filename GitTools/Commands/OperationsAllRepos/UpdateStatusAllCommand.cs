using GitTools.Git;
using Spectre.Console;
using System.Threading.Tasks;

namespace GitTools.Commands.OperationsAllRepos
{
    public class UpdateStatusAllCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            List<Task> tasks = [];
            Manager.RepositoryList.ForEach(async repo =>
            {
                tasks.Add(new Task(async () =>
                {
                    repo.IsClean = await GitOperations.IsRepoCleanAsync(repo.LocalPath);
                }));
            });
            tasks.ForEach(t => t.Start());
            AnsiConsole.Status().Start("Updating Status... Please wait", ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                Task.WhenAll(tasks).Wait();
                Manager.Save();
            });

            ShowStatus(tasks);

            return true;
        }
    }
}
