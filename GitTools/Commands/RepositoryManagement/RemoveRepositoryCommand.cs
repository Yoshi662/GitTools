using GitTools.Entities;
using Spectre.Console;

namespace GitTools.Commands.RepositoryManagement
{
    public class RemoveRepositoryCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            string selectedRepo = AnsiConsole.Prompt
               (new SelectionPrompt<string>()
               .Title("[bold underline green]Please select a Repository[/]" +
                   "\n[dim italic grey]Use the arrow keys and Enter to Select[/]")
               .AddChoices(Manager.RepositoryList.Select(r => r.LocalPath))
               .HighlightStyle(new Style(Color.White, null, Decoration.Underline)
               ));
            if (!AnsiConsole.Confirm($"This will remove the repository from the list\n{selectedRepo}\nDo you want to delete it?", false))
            {
                return false;
            }
            GitRepository repoToDelete = Manager.RepositoryList.Find(r => r.LocalPath == selectedRepo);
            Manager.RepositoryList.Remove(repoToDelete);
            return true;
        }
    }
}
