using GitTools.Entities;
using Spectre.Console;

namespace GitTools.Commands.OperationsAllRepos
{
    public class ListReposCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            new UpdateStatusAllCommand().Run();
            Table table = new();
            table.AddColumn("Path");
            table.AddColumn("Is Clean");
            table.AddColumn("Current Branch");
            table.AddColumn("Last Commit");
            foreach (GitRepository repo in Manager.RepositoryList)
            {
                table.AddRow($"[link]{repo.LocalPath}[/]", repo.IsClean ? "[green]Yes[/]" : "[red]No[/]", repo.CurrentBranch ?? "[red]unknown[/]", repo.LastCommit?.ToString("u") ?? "[red]unknown[/]");
            }
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("\n\n");
            Console.ReadKey();
            return true;
        }
    }
}
