using GitTools.Git;

namespace GitTools.Commands.OperationsSingleRepo
{
    public class FetchCommand : BaseSingleRepoCommand
    {
        public override bool Run()
        {
            bool result = GitOperations.FetchRepositoryAsync(SelectedRepo).Result;
            ShowResponse(result, "Fetch");
            return result;
        }
    }

}

