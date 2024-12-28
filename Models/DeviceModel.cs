namespace LanCraft.Models
{
    public class DeviceModel
    {
        public string DeviceName { get; set; }
        public string DeviceNickname { get; set; }
        public string DeviceImageUrl { get; set; }

        public string IPv4 { get; set; }
        public string IPv6 { get; set; }

        public bool HasWan { get; set; }
        public string WanAddress { get; set; }
        public string LinkSpeed { get; set; }

        public List<string> Ports { get; set; } = [];
    }
}
