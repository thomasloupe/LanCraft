using LanCraft.Models;

namespace LanCraft.Pages;

public partial class AddDevicePage : ContentPage
{
    public AddDevicePage()
    {
        InitializeComponent();

        // Example: show/hide WAN section automatically
        HasWan.CheckedChanged += (s, e) => {
            WanSection.IsVisible = e.Value; // e.Value = true/false
        };
    }

    private void OnAddDeviceClicked(object sender, EventArgs e)
    {
        // Basic validation
        // Add switch/case for later validations with specific return messages
        if (string.IsNullOrWhiteSpace(DeviceName.Text) || string.IsNullOrWhiteSpace(IPv4.Text))
        {
            DisplayAlert("Error", "Device Name and IPv4 are required.", "OK");
            return;
        }

        var device = new DeviceModel
        {
            DeviceImageUrl = DeviceImageUrl.Text,
            DeviceName = DeviceName.Text,
            DeviceNickname = DeviceNickname.Text,
            IPv4 = IPv4.Text,
            IPv6 = IPv6.Text,
            HasWan = HasWan.IsChecked,
            WanAddress = WanAddress.Text,
            LinkSpeed = LinkSpeed.Text
            // Ports, etc.
        };

        AppDataManager.AddDevice(device);
        DisplayAlert("Success", "Device added successfully!", "OK");

        // Optionally, navigate away or clear fields
        DeviceImageUrl.Text = string.Empty;
        DeviceName.Text = string.Empty;
    }
}
