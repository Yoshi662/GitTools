using GitTools.Entities;
using System.Diagnostics;

namespace GitTools.Git
{
    public static class GitOperations
    {
        public static bool Exists()
        {
            try
            {
                return RunGitCommand("", "--version").Success;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> PullRepositoryAsync(string localPath)
        {
            return RunGitCommand(localPath, "pull").Success;
        }

        public static async Task<bool> CleanRepositoryAsync(string localPath)
        {
            return RunGitCommand(localPath, "clean -fdx").Success;
        }

        public static async Task<bool> PushRepositoryAsync(string localPath)
        {
            return RunGitCommand(localPath, "push").Success;
        }

        public static async Task<bool> FetchRepositoryAsync(string localPath)
        {
            return RunGitCommand(localPath, "fetch").Success;
        }

        public static async Task<string> StatusRepositoryAsync(string localPath)
        {
            var result = RunGitCommand(localPath, "status");
            if (result.Success)
            {
                return result.Output.Trim();
            }
            else
            {
                return default;
            }
        }

        public static async Task<bool> AddAllFilesAndCommitAsync(string localPath, string commitMessage)
        {
            if (RunGitCommand(localPath, "add -A").Success)
                return RunGitCommand(localPath, $"commit -m \"{commitMessage}\"").Success;
            else
                return false;
        }

        public static async Task<bool> IsRepoCleanAsync(string localPath)
        {
            return RunGitCommand(localPath, "status --porcelain").Output == "";
        }

        public static async Task<bool> IsPathARepoAsync(string localPath)
        {
            return RunGitCommand(localPath, "rev-parse --is-inside-work-tree").Output.StartsWith("true");
        }

        public static async Task<string> GetTopLevelPathAsync(string localPath)
        {
            ProcessResponse response = RunGitCommand(localPath, "rev-parse --show-toplevel");
            return response.Success ? response.Output : "";
        }

        public static async Task<string> GetCurrentBranchAsync(string localPath)
        {
            ProcessResponse response = RunGitCommand(localPath, "branch --show-current");
            return response.Success ? response.Output.Trim() : "";
        }

        public static async Task<string> ListBranchAsync(string localPath)
        {
            return RunGitCommand(localPath, "branch --no-color").Output;
        }

        public static async Task<bool> CheckoutBranchAsync(string localPath, string branchName)
        {
            return RunGitCommand(localPath, $"checkout {branchName}").Success;
        }

        public static async Task<DateTime> GetDateOfLastCommitAsync(string localPath)
        {
            var processresult = RunGitCommand(localPath, "log -1 --format=\"%at\"");
            return processresult.Success ? 
                DateTimeOffset.FromUnixTimeSeconds(
                    Convert.ToInt64(
                        processresult.Output.Trim()))
                            .UtcDateTime : 
                DateTime.MinValue;
        }

        public static ProcessResponse RunGitCommand(string localPath, string args)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = localPath
                }
            };
            string output = "";
            string err = "";
            process.Start();
            output = process.StandardOutput.ReadToEnd();
            err = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return new ProcessResponse(output, err, process.ExitCode == 0);
        }
    }
}