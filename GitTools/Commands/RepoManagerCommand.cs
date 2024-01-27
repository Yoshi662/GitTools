 using GitTools.Entities;
using Spectre.Console;

namespace GitTools.Commands
{
    public abstract class RepoManagerCommand : ICommand
    {
        internal GitRepositoryManager Manager = GitRepositoryManager.Instance;
        public abstract bool Run();

        internal void ShowStatus(List<Task> lists)
        {
            int CompletedTasks = lists.Where(t => t.IsCompletedSuccessfully).Count();
            int FailedTasks = lists.Where(t => t.IsFaulted).Count();
                    
            AnsiConsole.MarkupLine($"[green] {CompletedTasks} tasks completed[/]");
            AnsiConsole.MarkupLine($"[red] {FailedTasks} tasks failed[/]");
            Console.ReadKey();
        }
    }
}
