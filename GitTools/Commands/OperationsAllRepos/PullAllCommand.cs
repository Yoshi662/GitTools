﻿using GitTools.Git;
using Spectre.Console;

namespace GitTools.Commands.OperationsAllRepos
{
    public class PullAllCommand : RepoManagerCommand
    {
        public override bool Run()
        {
            List<Task> tasks = [];
            foreach (var item in Manager.RepositoryList.Where(r => r.IsClean))
            {
                Task<bool> task =  Task.Run(async () =>
                {
                   return await GitOperations.PullRepositoryAsync(item.LocalPath);
                });
                tasks.Add(task);
            }

            AnsiConsole.Status().Start("Pulling All Repos... Please wait", ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
                Task.WhenAll(tasks).Wait();
            });

            ShowStatus(tasks);

            return true;
        }
    }
}
