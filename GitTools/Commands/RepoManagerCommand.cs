using GitTools.Entities;
using Spectre.Console;

namespace GitTools.Commands
{
    public abstract class RepoManagerCommand : ICommand
    {
        internal GitRepositoryManager Manager = GitRepositoryManager.Instance;
        public abstract bool Run();

        internal void ShowTasksResults(List<Task> lists)
        {
            int CompletedTasks = lists.Where(t => t.IsCompletedSuccessfully).Count();
            int FailedTasks = lists.Where(t => t.IsFaulted).Count();

            AnsiConsole.MarkupLine($"[green] {CompletedTasks} tasks completed[/]");
            AnsiConsole.MarkupLine($"[red] {FailedTasks} tasks failed[/]");
            Console.ReadKey();
        }

        internal void ShowStatus(List<Task> tasks, string status)
        {
            AnsiConsole.Status().Start(status, ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                Task.WhenAll(tasks).Wait();
            });
        }

        internal void ShowProgress(List<Task> tasks, string status)
        {
            AnsiConsole.Progress()
                .Columns(new ProgressColumn[] 
                {
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),        // Progress bar
                    new PercentageColumn(),         // Percentage
                    new RemainingTimeColumn(),      // Remaining time
                    new SpinnerColumn(),            // Spinner
                })
                .Start(ctx =>
                {
                   var x = ctx.AddTask(status);
                   x.MaxValue = tasks.Count;
                   while (!ctx.IsFinished)
                   {
                       x.Value = tasks.Where(t => t.IsCompletedSuccessfully).Count();
                       Task.Delay(50).Wait();
                   }
                });
        }
    }
}