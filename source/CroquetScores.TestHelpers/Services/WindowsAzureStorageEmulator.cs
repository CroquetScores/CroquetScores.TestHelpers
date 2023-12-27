using CroquetScores.TestHelpers.Support;

namespace CroquetScores.TestHelpers.Services
{
    public class WindowsAzureStorageEmulator
    {
        private static readonly WindowsAzureStorageEmulatorProcess Instance = new WindowsAzureStorageEmulatorProcess();

        public static void StartIfNotRunning()
        {
            Instance.StartIfNotRunning();
        }
    }
}