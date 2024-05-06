namespace ServerMonitoring.Application;

public class MailConfiguration
{
    public string SmtpServer { get; init; }
    public int SmtpPort { get; init; }
    public string SmtpUser { get; init; }
    public string SmtpPassword { get; init; }
}