namespace GitTools.Commands.OperationsSingleRepo
{
    public abstract class BaseSingleRepoCommand : ICommand
    {
        public string SelectedRepo { get; set; }

        public abstract bool Run();
    }
}
