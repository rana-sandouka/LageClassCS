    public class SkinFileInfo : INamedFileInfo, IHasPrimaryKey
    {
        public int ID { get; set; }

        public int SkinInfoID { get; set; }

        public int FileInfoID { get; set; }

        public FileInfo FileInfo { get; set; }

        [Required]
        public string Filename { get; set; }
    }