using GitTools.Entities;
using Spectre.Console;

namespace GitTools.Commands.RepositoryManagement
{
    public class ListReposCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            Table table = new();
            table.AddColumn("Path");
            table.AddColumn("Is Clean");
            foreach (GitRepository repo in Manager.RepositoryList)
            {
                table.AddRow($"[link]{repo.LocalPath}[/]", repo.IsClean ? "[green]Yes[/]" : "[red]No[/]");
            }
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("\n\n");
            Console.ReadKey();
            return true;
        }
    }
}
