using System.IO;
using System.Linq;

namespace CroquetScores.TestHelpers
{
    public static class Solution
    {
        public static readonly DirectoryInfo Directory = GetSolutionDirectory();
        public static readonly FileInfo IISExpressConfigFile = GetIISExpressConfigFile();

        private static FileInfo GetIISExpressConfigFile()
        {
            return new FileInfo($"{Directory.FullName}\\Scripts\\iis-express\\development.config");
        }

        private static DirectoryInfo GetSolutionDirectory()
        {
            DirectoryInfo solutionDirectory;

            if (FindSolutionDirectoryByAssembly(out solutionDirectory) ||
                FindSolutionDirectoryByKnownDirectories(out solutionDirectory))
            {
                return solutionDirectory;
            }

            throw new DirectoryNotFoundException("Cannot find solution directory.");
        }

        private static bool FindSolutionDirectoryByAssembly(out DirectoryInfo solutionDirectory)
        {
            var type = typeof(Projects);
            var assembly = type.Assembly;
            var location = assembly.Location;
            var configDirectory = new FileInfo(location).Directory; // e.g. Debug or Release
            var binDirectory = configDirectory.Parent;
            var projectDirectory = binDirectory.Parent;
            var projectsDirectory = projectDirectory.Parent;

            solutionDirectory = projectsDirectory.Parent;

            return IsSolutionDirectory(solutionDirectory);
        }

        private static bool FindSolutionDirectoryByKnownDirectories(out DirectoryInfo solutionDirectory)
        {
            // todo: improve this method to remove need to use knownDirectories.
            var knownDirectories = new[]
            {
                new DirectoryInfo(@"C:\Users\Tim\Code\scorelines\production\croquetscores.com"),
                new DirectoryInfo(@"C:\Users\Tim\Google Drive\Code\scorelines\production\croquetscores.com")
            };

            solutionDirectory = knownDirectories.SingleOrDefault(IsSolutionDirectory);

            return solutionDirectory != null;
        }

        private static bool IsSolutionDirectory(DirectoryInfo directory)
        {
            return directory.Exists && directory.EnumerateFiles("croquetscores.com.sln").Any();
        }
    }
}