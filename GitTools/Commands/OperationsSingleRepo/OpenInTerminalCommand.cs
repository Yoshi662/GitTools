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
                Process.Start($"wt.exe -d {SelectedRepo}");
                return true;
            }
            catch (Exception)
            {
                AnsiConsole.MarkupLine("[red]Failed to open terminal.[/]" +
                    "Make sure Windows Terminal is installed");
                return false;
            }
        }
    }

}

