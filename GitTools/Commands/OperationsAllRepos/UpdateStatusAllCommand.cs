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
                    repo.CurrentBranch = await GitOperations.GetCurrentBranchAsync(repo.LocalPath);
                    repo.LastCommit = await GitOperations.GetDateOfLastCommitAsync(repo.LocalPath);
                }));
            });
            tasks.ForEach(t => t.Start());
            ShowProgress(tasks, "Updating Status... Please wait");
            
            Manager.Save();

            return true;
        }
    }
}