using Spectre.Console;

namespace GitTools.Commands.OperationsSingleRepo
{
    public abstract class BaseSingleRepoCommand : ICommand
    {
        public string SelectedRepo { get; set; }

        public abstract bool Run();

        internal void ShowResponse(bool status, string action)
        {
            if (status)
            {
                AnsiConsole.MarkupLine($"[green]{action} completed[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{action} failed[/]");
            }
            Console.ReadKey();
        }
    }
}
