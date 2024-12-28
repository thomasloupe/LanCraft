using LanCraft.Models;
using System.Text.Json;

namespace LanCraft
{
    public static class AppDataManager
    {
        private static readonly string devicesFile;
        private static List<DeviceModel>? cachedDevices;

        static AppDataManager()
        {
            var appDataPath = FileSystem.AppDataDirectory;
            devicesFile = Path.Combine(appDataPath, "devices.json");

            if (!File.Exists(devicesFile))
            {
                File.WriteAllText(devicesFile, "[]");
            }
        }

        private static void LoadDevices()
        {
            if (cachedDevices == null)
            {
                var json = File.ReadAllText(devicesFile);
                cachedDevices = JsonSerializer.Deserialize<List<DeviceModel>>(json)
                               ?? [];
            }
        }

        private static void SaveDevices()
        {
            var json = JsonSerializer.Serialize(cachedDevices, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(devicesFile, json);
        }

        public static List<DeviceModel> GetAllDevices()
        {
            LoadDevices();
            return cachedDevices!;
        }

        public static void AddDevice(DeviceModel device)
        {
            LoadDevices();
            cachedDevices!.Add(device);
            SaveDevices();
        }

        public static void UpdateDevice(DeviceModel updated)
        {
            LoadDevices();
            var existing = cachedDevices!.FirstOrDefault(d => d.DeviceName == updated.DeviceName);
            if (existing != null)
            {
                // update properties
                existing.DeviceNickname = updated.DeviceNickname;
                existing.DeviceImageUrl = updated.DeviceImageUrl;
                existing.IPv4 = updated.IPv4;
                existing.IPv6 = updated.IPv6;
                existing.HasWan = updated.HasWan;
                existing.WanAddress = updated.WanAddress;
                existing.LinkSpeed = updated.LinkSpeed;
                // ...
            }
            SaveDevices();
        }

        public static void RemoveDevice(string deviceName)
        {
            LoadDevices();
            var existing = cachedDevices!.FirstOrDefault(d => d.DeviceName == deviceName);
            if (existing != null)
            {
                cachedDevices.Remove(existing);
                SaveDevices();
            }
        }
    }
}