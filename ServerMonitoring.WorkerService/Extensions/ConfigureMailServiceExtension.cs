using System.Net;
using System.Net.Mail;
using ServerMonitoring.Application;

namespace ServerMonitoring.WorkerService.Extensions;

public static class ConfigureMailServiceExtension
{
    public static void ConfigureMailService(this IServiceCollection services, IConfiguration configuration)
    {
        var mailConfiguration = configuration.GetSection("MailConfiguration").Get<MailConfiguration>();
        if (mailConfiguration.SmtpServer == null || mailConfiguration.SmtpUser == null ||
            mailConfiguration.SmtpPassword == null)
        {
            throw new Exception("Mail configuration is not set up correctly.");
        }

        services.AddSingleton<SmtpClient>(_ =>
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = mailConfiguration.SmtpServer;
            smtpClient.Port = mailConfiguration.SmtpPort;
            smtpClient.Credentials = new NetworkCredential(mailConfiguration.SmtpUser, mailConfiguration.SmtpPassword);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            return smtpClient;
        });
        services.AddTransient<MailService>();
    }
}