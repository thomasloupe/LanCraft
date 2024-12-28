namespace LanCraft.Pages;

public partial class VisualizePage : ContentPage
{
    public VisualizePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Load devices to display
        var devices = AppDataManager.GetAllDevices();
        DevicesCollectionView.ItemsSource = devices;
    }
}
