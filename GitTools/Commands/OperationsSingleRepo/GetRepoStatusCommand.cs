using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.OperationsSingleRepo
{
    public class GetRepoStatusCommand : BaseSingleRepoCommand
    {
        public override bool Run()
        {
            AnsiConsole.MarkupLine(GitOperations.StatusRepositoryAsync(SelectedRepo).Result);
            Console.ReadKey();
            return true;
        }
    }
}

