using CroquetScores.TestHelpers.Support;

namespace CroquetScores.TestHelpers.Services
{
    internal class WindowsAzureStorageEmulator
    {
        private static readonly WindowsAzureStorageEmulatorProcess Instance = new WindowsAzureStorageEmulatorProcess();

        internal static void StartIfNotRunning()
        {
            Instance.StartIfNotRunning();
        }
    }
}