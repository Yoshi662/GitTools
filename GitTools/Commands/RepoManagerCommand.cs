using GitTools.Entities;

namespace GitTools.Commands
{
    public abstract class RepoManagerCommand : ICommand
    {
        internal GitRepositoryManager Manager = GitRepositoryManager.Instance;
        public abstract bool Run();
    }
}
