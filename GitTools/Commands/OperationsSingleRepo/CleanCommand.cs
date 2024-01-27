using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.OperationsSingleRepo
{
    public class CleanCommand : BaseSingleRepoCommand
    {
        public override bool Run()
        {
            bool result = GitOperations.CleanRepositoryAsync(SelectedRepo).Result;
            ShowResponse(result, "Clean");
            return true;
        }
    }

}

