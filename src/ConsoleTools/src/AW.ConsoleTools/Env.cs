using System.Reflection;

namespace AW.ConsoleTools
{
    public static class Env
    {
        static Env()
        {
            
        }

        public static string RepositoryRoot
        {
            get
            {
                var directoryInfo = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);

                while (!File.Exists(Path.Combine(directoryInfo.FullName, "README.md")))
                {
                    if (directoryInfo.Parent == null)
                    {
                        return string.Empty;
                    }

                    directoryInfo = directoryInfo.Parent;
                }

                string repositoryRoot = directoryInfo.FullName;
                return repositoryRoot;
            }
        }
    }
}