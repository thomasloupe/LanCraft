using System.Text.Json;

namespace LanCraft
{
    public static class SettingsManager
    {
        private static readonly string settingsFile = Path.Combine(FileSystem.AppDataDirectory, "settings.json");

        public static void SaveSettings(AppSettings settings)
        {
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsFile, json);
        }

        public static AppSettings LoadSettings()
        {
            if (!File.Exists(settingsFile))
                return new AppSettings(); // default
            var json = File.ReadAllText(settingsFile);
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }
    }

    public class AppSettings
    {
        public string Theme { get; set; } = "System";
        public int WindowWidth { get; set; } = 1300;
        public int WindowHeight { get; set; } = 900;
        // Other settings as needed
    }
}