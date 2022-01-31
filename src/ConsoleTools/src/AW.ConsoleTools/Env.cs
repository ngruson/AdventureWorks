using System.Reflection;

namespace AW.ConsoleTools
{
    public static class Env
    {
        static Env()
        {
            RepositoryRoot = "";
            var directoryInfo = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);

            while (!File.Exists(Path.Combine(directoryInfo.FullName, "README.md")))
            {
                if (directoryInfo.Parent == null)
                {
                    return;
                }

                directoryInfo = directoryInfo.Parent;
            }

            RepositoryRoot = directoryInfo.FullName;
        }

        public static string RepositoryRoot { get; }
    }
}