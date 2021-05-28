using System.IO;
using OpenMagic.Assertions;

namespace CroquetScores.TestHelpers
{
    public static class Projects
    {
        public static readonly DirectoryInfo Directory = GetProjectsDirectory();

        private static DirectoryInfo GetProjectsDirectory()
        {
            var projectsDirectory = new DirectoryInfo(Path.Combine(Solution.Directory.FullName, "Projects"));

            projectsDirectory.Exists.MustBeTrue("Cannot find projects directory. Expected it to be {0}.", projectsDirectory.FullName);

            return projectsDirectory;
        }

        public static class CroquetScoresWeb
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static readonly DirectoryInfo Directory = GetProjectDirectory();

            private static DirectoryInfo GetProjectDirectory()
            {
                var directory = new DirectoryInfo(Path.Combine(Projects.Directory.FullName, "CroquetScores.Web"));

                directory.Exists.MustBeTrue("Cannot find CroquetScores.Web project directory. Expected it to be {0}.", directory.FullName);

                return directory;
            }
        }
    }
}