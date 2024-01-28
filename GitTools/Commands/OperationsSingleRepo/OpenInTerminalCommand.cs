using Spectre.Console;
using System.Diagnostics;

namespace GitTools.Commands.OperationsSingleRepo
{
    public class OpenInTerminalCommand : BaseSingleRepoCommand
    {
        public override bool Run()
        {
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "wt.exe",
                    Arguments = $@"-d {SelectedRepo}",
                    UseShellExecute = false
                });
                return true;
            }
            catch (Exception)
            {
                AnsiConsole.MarkupLine("[red]Failed to open terminal.[/]" +
                    "Make sure Windows Terminal is installed");
                Console.ReadKey();
                return false;
            }
        }
    }

}

