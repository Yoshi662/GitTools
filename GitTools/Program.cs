using GitTools.Screens;
using Spectre.Console;

IScreen Main = new MainScreen();

if (!System.Diagnostics.Debugger.IsAttached)
{
    try
    {
        Main.Show();
    }
    catch (Exception e)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[rapidblink bold white on red]SOMETHING WRONG HAPPENED.[/]" +
            "\n[underline darkblue link=https://github.com/Yoshi662]BLAME YOSHI ON GITHUB[/]" +
            "\n[italic grey]Send him this brick of text or screenshot plz[/]" +
            "\n\n\n");
        AnsiConsole.WriteException(e);
        AnsiConsole.Confirm("\nPress yes or no to close");
    }
}
else
{
    Main.Show();
}