using GitTools.Commands;
using GitTools.Entities;
using GitTools.Screens;
using System.Configuration;

namespace GitTools.IntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddALotOfRepos()
        {
            ConfigurationManager.AppSettings["ReposFilePath"] = "reposTest.json";

            GitArrenger arrenger = new();

            for (int i = 0; i < Random.Shared.Next(5,20); i++)
            {
                if(Random.Shared.Next(10) < 5)
                {
                    var repo = new GitRepository(arrenger.CreateCleanRepo(), true, "main");
                    GitRepositoryManager.Instance.RepositoryList.Add(repo);
                } else
                {
                    var repo = new GitRepository(arrenger.CreateDirtyRepo(), false, "main");
                    GitRepositoryManager.Instance.RepositoryList.Add(repo);
                }
            }
            GitRepositoryManager.Instance.Save();
            
        }
    }
}