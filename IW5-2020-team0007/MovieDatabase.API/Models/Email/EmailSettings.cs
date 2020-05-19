namespace MovieDatabase.API.Models.Email
{
    public class EmailSettings
    {
        public string SmtpAddress { get; set; }
        public int Port { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SenderAddress { get; set; }
    }
}
