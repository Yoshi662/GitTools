using GitTools.Menus;

namespace GitTools.Screens
{
    public class DummyScreen : IScreen
    {
        public void Show()
        {
            List<MenuOption> options = [
                                new ExitOption(),
                new ExitOption(),
                new ExitOption(),
                new ExitOption(),
                ];

            new Menu(options).AskAndSelect();
        }
    }
}
