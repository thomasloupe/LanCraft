namespace LanCraft.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Load the current settings (placeholder)
        var settings = SettingsManager.LoadSettings();

        // Example mapping
        ThemePicker.SelectedIndex = settings.Theme switch
        {
            "Light" => 0,
            "Dark" => 1,
            _ => 2,
        };
        EntryWidth.Text = settings.WindowWidth.ToString();
        EntryHeight.Text = settings.WindowHeight.ToString();
    }

    private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // optionally preview theme changes in real time
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Validate numeric input
        if (!int.TryParse(EntryWidth.Text, out int newWidth))
            newWidth = 1300;
        if (!int.TryParse(EntryHeight.Text, out int newHeight))
            newHeight = 900;

        // Grab selected theme
        var theme = "System";
        if (ThemePicker.SelectedIndex == 0) theme = "Light";
        else if (ThemePicker.SelectedIndex == 1) theme = "Dark";

        // Save
        var settings = new AppSettings
        {
            Theme = theme,
            WindowWidth = newWidth,
            WindowHeight = newHeight,
            // etc
        };
        SettingsManager.SaveSettings(settings);

        await DisplayAlert("Settings", "Saved successfully!", "OK");
    }
}
