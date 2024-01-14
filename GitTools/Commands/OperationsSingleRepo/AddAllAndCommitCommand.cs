using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.OperationsSingleRepo
{
    public class AddAllAndCommitCommand : BaseSingleRepoCommand
    {
        public override bool Run()
        {
            string commitMessage = AnsiConsole.Ask<string>("Enter your commit message: ");

            bool result = GitOperations.AddAllFilesAndCommitAsync(SelectedRepo, commitMessage).Result;       
            ShowResponse(result, "Commit");
            return result;
        }
    }

}

