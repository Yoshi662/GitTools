using System.Configuration;
using System.Text.Json;

namespace GitTools.Entities
{
    public class GitRepositoryManager
    {
        private static GitRepositoryManager _instance;
        private static readonly object _lock = new object();

        private string _repoPath = ConfigurationManager.AppSettings.Get("ReposFilePath");

        private JsonSerializerOptions _serializerOptions = new()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        public List<GitRepository> RepositoryList { get; set; }

        private GitRepositoryManager()
        {
            RepositoryList = new List<GitRepository>();
            if (!File.Exists(_repoPath)) File.WriteAllText(_repoPath, "[]");
            Load();
            CleanCurrentListAndSave();
        }

        public static GitRepositoryManager Instance => _instance ??= new GitRepositoryManager();

        public void Load()
        {
            RepositoryList = JsonSerializer.Deserialize<List<GitRepository>>(
                File.ReadAllText(_repoPath), _serializerOptions) ?? [];
        }

        public void Save()
        {
            File.WriteAllText(_repoPath, JsonSerializer.Serialize(RepositoryList, _serializerOptions));
        }

        public void CleanCurrentListAndSave()
        {
            //Sanitizes paths
            RepositoryList.ForEach(r => r.LocalPath = Path.GetFullPath(r.LocalPath));
            //Removes duplicates and sorts
            if (RepositoryList != RepositoryList.Distinct().ToList())
            {
                RepositoryList = RepositoryList.Distinct().ToList();
            }
            RepositoryList.Sort
            (
                (r1, r2) => String.Compare(r1.LocalPath, r2.LocalPath, StringComparison.Ordinal)
            );
            Save();
        }
    }
}
