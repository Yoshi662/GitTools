using GitTools.Git;

namespace GitTools.IntegrationTests
{
    public class GitArrenger
    {
        private string CreateGitRepo()
        {
            string repo = Directory.CreateTempSubdirectory().FullName;
            GitOperations.RunGitCommand(repo, "init");
            return repo;
        }

        private void AddRandomFile(string path)
        {
            var filename = Path.Combine(path, Path.GetRandomFileName());
            var stream = File.CreateText(filename);
            byte[] buffer = [128];
            Random.Shared.NextBytes(buffer);
            string base64String = Convert.ToBase64String(buffer);
            stream.Write(base64String);
            stream.Close();
        }

        private void AutoAddAllFilesAndCommit(string path)
        {
            GitOperations.RunGitCommand(path, "add -A");
            GitOperations.RunGitCommand(path, $"commit -m \"AutoCommit: {DateTime.Now:s}\"");
        }

        public string CreateEmptyRepo()
        {
            return CreateGitRepo();
        }

        public string CreateCleanRepo()
        {
            string repo = CreateGitRepo();

            for (int i = 0; i < Random.Shared.Next(1, 10); i++)
            {
                for (int o = 0; o < Random.Shared.Next(1, 10); o++)
                {
                    AddRandomFile(repo);
                }
                AutoAddAllFilesAndCommit(repo);
            }

            return repo;
        }

        public string CreateDirtyRepo()
        {
            string repo = CreateGitRepo();

            for (int i = 0; i < Random.Shared.Next(1, 10); i++)
            {
                for (int o = 0; o < Random.Shared.Next(1, 10); o++)
                {
                    AddRandomFile(repo);
                }
                AutoAddAllFilesAndCommit(repo);
            }

            for (int o = 0; o < Random.Shared.Next(1, 10); o++)
            {
                AddRandomFile(repo);
            }

            return repo;
        }

        public void DeleteRepo(string path) => Directory.Delete(path, true);
    }
}
