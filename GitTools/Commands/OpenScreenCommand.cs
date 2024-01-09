using GitTools.Screens;

namespace GitTools.Commands
{
    public class OpenScreenCommand : ICommand
    {
        IScreen Screen;

        public OpenScreenCommand(IScreen screen)
        {
            Screen = screen;
        }

        public bool Run()
        {
            Screen.Show();
            return true;
        }
    }
}
