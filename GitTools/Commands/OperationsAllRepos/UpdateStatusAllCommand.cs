using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.OperationsAllRepos
{
    public class UpdateStatusAllCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            AnsiConsole.Status().Start("Updating Status... Please wait", ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                Manager.RepositoryList.ForEach(async repo =>
                {
                    repo.IsClean = await GitOperations.IsRepoCleanAsync(repo.LocalPath);
                });
                Manager.Save();
            });
            return true;
        }
    }
}
