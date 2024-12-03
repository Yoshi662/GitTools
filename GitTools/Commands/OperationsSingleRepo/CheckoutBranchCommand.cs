using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.OperationsSingleRepo;

public class CheckoutBranchCommand : BaseSingleRepoCommand
{
    public override bool Run()
    {
        string checkoutbranch = AnsiConsole.Ask<string>("Enter the Branch Path", "master");
        bool result = GitOperations.CheckoutBranchAsync(SelectedRepo, checkoutbranch).Result;
        ShowResponse(result, "Changed branch");
        return true;
    }
}