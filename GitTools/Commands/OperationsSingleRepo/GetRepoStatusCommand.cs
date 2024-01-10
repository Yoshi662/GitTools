using Spectre.Console;

namespace GitTools.Commands.OperationsSingleRepo
{
    public class GetRepoStatusCommand : BaseSingleRepoCommand
    {
        public override bool Run()
        {
            AnsiConsole.WriteLine("SOME STATUS HERE");
            Console.ReadKey();
            return true;
        }
    }
}
