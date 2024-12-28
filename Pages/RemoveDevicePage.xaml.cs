using LanCraft.Models;

namespace LanCraft.Pages;

public partial class RemoveDevicePage : ContentPage
{
    private List<DeviceModel> devices;

    public RemoveDevicePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Load devices into the picker
        devices = AppDataManager.GetAllDevices();
        DevicePicker.ItemsSource = devices;
    }

    private async void OnRemoveDeviceClicked(object sender, EventArgs e)
    {
        if (DevicePicker.SelectedIndex < 0)
        {
            await DisplayAlert("Error", "Please select a device to remove.", "OK");
            return;
        }

        var selectedDevice = (DeviceModel)DevicePicker.SelectedItem;

        // Confirm removal
        bool confirmed = await DisplayAlert(
            "Confirm Removal",
            $"Are you sure you want to remove '{selectedDevice.DeviceName}'?",
            "Yes",
            "No"
        );

        if (!confirmed) return;

        // Remove the device
        AppDataManager.RemoveDevice(selectedDevice.DeviceName);

        await DisplayAlert("Success", $"{selectedDevice.DeviceName} removed successfully!", "OK");

        // Refresh the list
        devices = AppDataManager.GetAllDevices();
        DevicePicker.ItemsSource = devices;
        DevicePicker.SelectedIndex = -1;
    }
}
