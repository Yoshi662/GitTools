using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.OperationsSingleRepo;

public class ListBranchCommand : BaseSingleRepoCommand
{
    public override bool Run()
    {
        string branchlist = GitOperations.ListBranchAsync(SelectedRepo).Result;
        AnsiConsole.WriteLine($"Branch list:\n{branchlist}");
        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return true;
    }
}