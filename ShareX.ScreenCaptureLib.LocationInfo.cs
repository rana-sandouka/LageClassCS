    public class LocationInfo
    {
        public long Location { get; set; }
        public long Length { get; set; }

        public LocationInfo(long location, long length)
        {
            Location = location;
            Length = length;
        }
    }