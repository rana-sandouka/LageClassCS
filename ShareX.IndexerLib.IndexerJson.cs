    public class IndexerJson : Indexer
    {
        private JsonWriter jsonWriter;

        public IndexerJson(IndexerSettings indexerSettings) : base(indexerSettings)
        {
        }

        public override string Index(string folderPath)
        {
            FolderInfo folderInfo = GetFolderInfo(folderPath);
            folderInfo.Update();

            StringBuilder sbContent = new StringBuilder();

            using (StringWriter sw = new StringWriter(sbContent))
            using (jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = Formatting.Indented;

                jsonWriter.WriteStartObject();
                IndexFolder(folderInfo);
                jsonWriter.WriteEndObject();
            }

            return sbContent.ToString();
        }

        protected override void IndexFolder(FolderInfo dir, int level = 0)
        {
            if (settings.CreateParseableJson)
            {
                IndexFolderParseable(dir, level);
            }
            else
            {
                IndexFolderSimple(dir, level);
            }
        }

        private void IndexFolderSimple(FolderInfo dir, int level)
        {
            jsonWriter.WritePropertyName(dir.FolderName);
            jsonWriter.WriteStartArray();

            foreach (FolderInfo subdir in dir.Folders)
            {
                jsonWriter.WriteStartObject();
                IndexFolder(subdir);
                jsonWriter.WriteEndObject();
            }

            foreach (FileInfo fi in dir.Files)
            {
                jsonWriter.WriteValue(fi.Name);
            }

            jsonWriter.WriteEnd();
        }

        private void IndexFolderParseable(FolderInfo dir, int level)
        {
            jsonWriter.WritePropertyName("Name");
            jsonWriter.WriteValue(dir.FolderName);

            if (settings.ShowSizeInfo)
            {
                jsonWriter.WritePropertyName("Size");
                jsonWriter.WriteValue(dir.Size.ToSizeString(settings.BinaryUnits));
            }

            if (dir.Folders.Count > 0)
            {
                jsonWriter.WritePropertyName("Folders");
                jsonWriter.WriteStartArray();

                foreach (FolderInfo subdir in dir.Folders)
                {
                    jsonWriter.WriteStartObject();
                    IndexFolder(subdir);
                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEnd();
            }

            if (dir.Files.Count > 0)
            {
                jsonWriter.WritePropertyName("Files");
                jsonWriter.WriteStartArray();

                foreach (FileInfo fi in dir.Files)
                {
                    jsonWriter.WriteStartObject();

                    jsonWriter.WritePropertyName("Name");
                    jsonWriter.WriteValue(fi.Name);

                    if (settings.ShowSizeInfo)
                    {
                        jsonWriter.WritePropertyName("Size");
                        jsonWriter.WriteValue(fi.Length.ToSizeString(settings.BinaryUnits));
                    }

                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEnd();
            }
        }
    }