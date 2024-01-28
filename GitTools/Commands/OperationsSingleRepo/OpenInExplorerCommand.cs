using Spectre.Console;
using System.Diagnostics;

namespace GitTools.Commands.OperationsSingleRepo
{
    public class OpenInExplorerCommand : BaseSingleRepoCommand
    {
        public override bool Run()
        {
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "Explorer.exe",
                    Arguments = $@"{SelectedRepo}",
                    UseShellExecute = false
                });
                return true;
            }
            catch (Exception)
            {
                AnsiConsole.MarkupLine("[red]Failed to open explorer.[/]");
                return false;
            }
        }
    }

}

