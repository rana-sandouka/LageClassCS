    public class EmailFileUploaderService : FileUploaderService
    {
        public override FileDestination EnumValue { get; } = FileDestination.Email;

        public override Image ServiceImage => Resources.mail;

        public override bool CheckConfig(UploadersConfig config)
        {
            return !string.IsNullOrEmpty(config.EmailSmtpServer) && config.EmailSmtpPort > 0 && !string.IsNullOrEmpty(config.EmailFrom) && !string.IsNullOrEmpty(config.EmailPassword);
        }

        public override GenericUploader CreateUploader(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            if (config.EmailAutomaticSend && !string.IsNullOrEmpty(config.EmailAutomaticSendTo))
            {
                return new Email()
                {
                    SmtpServer = config.EmailSmtpServer,
                    SmtpPort = config.EmailSmtpPort,
                    FromEmail = config.EmailFrom,
                    Password = config.EmailPassword,
                    ToEmail = config.EmailAutomaticSendTo,
                    Subject = config.EmailDefaultSubject,
                    Body = config.EmailDefaultBody
                };
            }
            else
            {
                using (EmailForm emailForm = new EmailForm(config.EmailRememberLastTo ? config.EmailLastTo : "", config.EmailDefaultSubject, config.EmailDefaultBody))
                {
                    if (emailForm.ShowDialog() == DialogResult.OK)
                    {
                        if (config.EmailRememberLastTo)
                        {
                            config.EmailLastTo = emailForm.ToEmail;
                        }

                        return new Email()
                        {
                            SmtpServer = config.EmailSmtpServer,
                            SmtpPort = config.EmailSmtpPort,
                            FromEmail = config.EmailFrom,
                            Password = config.EmailPassword,
                            ToEmail = emailForm.ToEmail,
                            Subject = emailForm.Subject,
                            Body = emailForm.Body
                        };
                    }
                    else
                    {
                        taskInfo.StopRequested = true;
                    }
                }
            }

            return null;
        }

        public override TabPage GetUploadersConfigTabPage(UploadersConfigForm form) => form.tpEmail;
    }