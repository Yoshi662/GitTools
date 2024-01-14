using GitTools.Git;

namespace GitTools.Commands.OperationsSingleRepo
{
    public class PullCommand : BaseSingleRepoCommand
    {
        public override bool Run()
        {
            bool result = GitOperations.PullRepositoryAsync(SelectedRepo).Result;
            ShowResponse(result, "Pull");
            return result;
        }
    }

}

