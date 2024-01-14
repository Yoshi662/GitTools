using GitTools.Git;

namespace GitTools.Commands.OperationsSingleRepo
{
    public class PushCommand : BaseSingleRepoCommand
    {
        public override bool Run()
        {
            bool result = GitOperations.PushRepositoryAsync(SelectedRepo).Result;
            ShowResponse(result, "Push");
            return result;
        }
    }

}

