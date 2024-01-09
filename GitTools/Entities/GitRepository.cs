namespace GitTools.Entities
{
    public class GitRepository
    {
        public string LocalPath { get; set; }
        public bool IsClean { get; set; }
        public GitRepository() { }
        public GitRepository(string localPath, bool isClean)
        {
            LocalPath = localPath;
            IsClean = isClean;
        }
    }
}
