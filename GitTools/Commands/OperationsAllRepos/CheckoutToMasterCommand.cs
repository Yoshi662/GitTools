using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.OperationsAllRepos
{
    public class CheckoutToMasterCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            List<Task> tasks = [];
            foreach (var item in Manager.RepositoryList.Where(r => r.IsClean))
            {
                Task<bool> task =  Task.Run(async () =>
                {
                   bool trytomaster = await GitOperations.CheckoutBranchAsync(item.LocalPath, "master");
                   if (trytomaster)
                       return true;

                   return await GitOperations.CheckoutBranchAsync(item.LocalPath, "main");
                });
                tasks.Add(task);
            }

            AnsiConsole.Status().Start("Checkout to master in All Repos... Please wait", ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                Task.WhenAll(tasks).Wait();
            });

            ShowStatus(tasks);

            return true;
        }
    }
}
