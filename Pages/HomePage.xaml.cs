namespace LanCraft.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Example: load stats from some data source
        int totalDevices = AppDataManager.GetAllDevices().Count;
        int wanDevices = AppDataManager.GetAllDevices().Count(d => d.HasWan);
        int totalPorts = AppDataManager.GetAllDevices().Sum(d => d.Ports?.Count ?? 0);

        LabelTotalDevices.Text = $"Total Devices: {totalDevices}";
        LabelWanDevices.Text = $"WAN Devices: {wanDevices}";
        LabelTotalPorts.Text = $"Total Ports: {totalPorts}";
    }
}
