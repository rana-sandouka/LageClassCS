    public class UpnpDeviceInfo
    {
        public Uri Location { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public IPAddress LocalIpAddress { get; set; }

        public int LocalPort { get; set; }
    }