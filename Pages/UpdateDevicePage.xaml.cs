using LanCraft.Models;

namespace LanCraft.Pages;

public partial class UpdateDevicePage : ContentPage
{
    private List<DeviceModel> devices;

    public UpdateDevicePage()
    {
        InitializeComponent();

        // Show/hide WAN section automatically when checkbox changes
        HasWan.CheckedChanged += (s, e) => {
            WanSection.IsVisible = e.Value;
        };
    }

    // When the page appears, load the current list of devices
    protected override void OnAppearing()
    {
        base.OnAppearing();

        devices = AppDataManager.GetAllDevices();
        DevicePicker.ItemsSource = devices;
    }

    // When user picks a device, populate the form fields
    private void DevicePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DevicePicker.SelectedIndex < 0) return;
        var selectedDevice = (DeviceModel)DevicePicker.SelectedItem;

        // Populate fields
        DeviceNickname.Text = selectedDevice.DeviceNickname;
        IPv4.Text = selectedDevice.IPv4;
        IPv6.Text = selectedDevice.IPv6;
        HasWan.IsChecked = selectedDevice.HasWan;
        WanAddress.Text = selectedDevice.WanAddress;
        LinkSpeed.Text = selectedDevice.LinkSpeed;

        WanSection.IsVisible = selectedDevice.HasWan;
    }

    private async void OnUpdateDeviceClicked(object sender, EventArgs e)
    {
        // Ensure a device is selected
        if (DevicePicker.SelectedIndex < 0)
        {
            await DisplayAlert("Error", "Please select a device to update.", "OK");
            return;
        }

        var selectedDevice = (DeviceModel)DevicePicker.SelectedItem;

        // Basic validation example
        if (string.IsNullOrWhiteSpace(IPv4.Text))
        {
            await DisplayAlert("Error", "IPv4 is required.", "OK");
            return;
        }

        // Update the device
        selectedDevice.DeviceNickname = DeviceNickname.Text;
        selectedDevice.IPv4 = IPv4.Text;
        selectedDevice.IPv6 = IPv6.Text;
        selectedDevice.HasWan = HasWan.IsChecked;
        selectedDevice.WanAddress = WanAddress.Text;
        selectedDevice.LinkSpeed = LinkSpeed.Text;

        // Persist changes
        AppDataManager.UpdateDevice(selectedDevice);

        await DisplayAlert("Success", $"{selectedDevice.DeviceName} updated successfully!", "OK");
    }
}
