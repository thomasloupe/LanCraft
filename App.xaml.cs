using LanCraft;

namespace LanCraft;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Switch from "new MainPage()" to "new AppShell()"
        MainPage = new AppShell();
    }
}
