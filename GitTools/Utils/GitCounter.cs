namespace GitTools.Tools
{
    public static class GitCounter
    {
        private static int counter = 0;
        public static void ShowNext()
        {
            counter++;
            System.Console.WriteLine($"Next: {counter}");
        }
    }
}
