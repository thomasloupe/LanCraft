namespace LanCraft.Pages;

public partial class TestNetworkPage : ContentPage
{
    public class NetworkTestResult
    {
        public string DeviceName { get; set; }
        public string Status { get; set; }  // e.g. "Online" or "Offline"
    }

    public TestNetworkPage()
    {
        InitializeComponent();
    }

    private async void OnTestNetworkClicked(object sender, EventArgs e)
    {
        // Acquire devices
        var devices = AppDataManager.GetAllDevices();

        // Placeholder results list
        var results = new List<NetworkTestResult>();

        // Example: Simple cross-platform attempt
        // On Windows & macOS, System.Net.NetworkInformation.Ping often works.
        // On Android/iOS, it may fail or require extra permissions.
        foreach (var device in devices)
        {
            if (string.IsNullOrEmpty(device.IPv4))
            {
                results.Add(new NetworkTestResult
                {
                    DeviceName = device.DeviceName,
                    Status = "No IPv4 configured"
                });
                continue;
            }

            var status = await IsDeviceReachable(device.IPv4)
                ? "Online"
                : "Offline or Timeout";

            results.Add(new NetworkTestResult
            {
                DeviceName = device.DeviceName,
                Status = status
            });
        }

        // Show the results
        ResultsCollectionView.ItemsSource = results;
    }

    // Example "ping" style check
    // This uses System.Net.NetworkInformation.Ping, which may or may not fully work on all MAUI platforms.
    // Adjust or wrap in platform-specific logic as needed.
    private static Task<bool> IsDeviceReachable(string ipAddress)
    {
#if WINDOWS || MACCATALYST
        return Task.Run(() =>
        {
            try
            {
                var ping = new System.Net.NetworkInformation.Ping();
                var reply = ping.Send(ipAddress, 1000);
                return (reply.Status == System.Net.NetworkInformation.IPStatus.Success);
            }
            catch
            {
                return false;
            }
        });
#else
        // For Android/iOS, do a different approach or skip:
        return Task.FromResult(false);
#endif
    }
}
