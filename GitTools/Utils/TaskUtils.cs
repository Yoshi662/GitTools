namespace GitTools.Utils
{
    public static class TaskUtils
    {
        public static void RunAllTasks(List<Task> tasks)
        {
            foreach (Task task in tasks)
                if (task.Status == TaskStatus.Created)
                    task.Start();

            Task.WhenAll(tasks).Wait();
        }

    }
}
