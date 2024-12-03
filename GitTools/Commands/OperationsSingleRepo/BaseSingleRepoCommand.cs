using Spectre.Console;

namespace GitTools.Commands.OperationsSingleRepo
{
    public abstract class BaseSingleRepoCommand : ICommand
    {
        public string SelectedRepo { get; set; }

        public abstract bool Run();

        internal void ShowResponse(bool status, string action)
        {
            AnsiConsole.MarkupLine(status ? $"[green]{action} completed[/]" : $"[red]{action} failed[/]");
            Console.ReadKey();
        }
    }
}
