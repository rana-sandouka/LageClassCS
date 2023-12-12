    public class Email : FileUploader
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string FromEmail { get; set; }
        public string Password { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public void Send()
        {
            Send(ToEmail, Subject, Body);
        }

        public void Send(string toEmail, string subject, string body)
        {
            Send(toEmail, subject, body, null, null);
        }

        public void Send(string toEmail, string subject, string body, Stream stream, string fileName)
        {
            using (SmtpClient smtp = new SmtpClient()
            {
                Host = SmtpServer,
                Port = SmtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromEmail, Password)
            })
            {
                using (MailMessage message = new MailMessage(FromEmail, toEmail))
                {
                    message.Subject = subject;
                    message.Body = body;

                    if (stream != null)
                    {
                        Attachment attachment = new Attachment(stream, fileName);
                        message.Attachments.Add(attachment);
                    }

                    smtp.Send(message);
                }
            }
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            Send(ToEmail, Subject, Body, stream, fileName);
            return new UploadResult { IsURLExpected = false };
        }
    }