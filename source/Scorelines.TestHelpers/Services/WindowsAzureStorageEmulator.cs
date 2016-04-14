using Scorelines.TestHelpers.Support;

namespace Scorelines.TestHelpers.Services
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
