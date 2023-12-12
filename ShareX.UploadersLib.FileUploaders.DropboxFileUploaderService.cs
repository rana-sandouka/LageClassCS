    public class DropboxFileUploaderService : FileUploaderService
    {
        public override FileDestination EnumValue { get; } = FileDestination.Dropbox;

        public override Icon ServiceIcon => Resources.Dropbox;

        public override bool CheckConfig(UploadersConfig config)
        {
            return OAuth2Info.CheckOAuth(config.DropboxOAuth2Info);
        }

        public override GenericUploader CreateUploader(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new Dropbox(config.DropboxOAuth2Info)
            {
                UploadPath = NameParser.Parse(NameParserType.Default, Dropbox.VerifyPath(config.DropboxUploadPath)),
                AutoCreateShareableLink = config.DropboxAutoCreateShareableLink,
                UseDirectLink = config.DropboxUseDirectLink
            };
        }

        public override TabPage GetUploadersConfigTabPage(UploadersConfigForm form) => form.tpDropbox;
    }