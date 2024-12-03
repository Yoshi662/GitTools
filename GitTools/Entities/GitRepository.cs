namespace GitTools.Entities
{
    public class GitRepository
    {
        public string LocalPath { get; set; }
        public bool IsClean { get; set; }
        public string? CurrentBranch { get; set; }
        public GitRepository() { }
        public GitRepository(string localPath, bool isClean, string currentBranch)
        {
            LocalPath = localPath;
            IsClean = isClean;
            CurrentBranch = currentBranch;
        }
    }
}
