using GitTools.Commands;
using Spectre.Console;

namespace GitTools.Menus
{
    public class ExitOption : MenuOption
    {
        public ExitOption() : base("Exit", new ExitCommand())
        {
            base.OptionStyle = new Style(Color.Red, null, Decoration.Italic);
        }
    }
}
