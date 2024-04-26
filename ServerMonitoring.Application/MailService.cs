using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace ServerMonitoring.Application;

public class MailService(SmtpClient smtpClient, IConfiguration configuration)
{
    // TODO check if job queue should be implemented for this "school" project
    public void SendMail(string subject, string body)
    {
        var from = configuration["MailConfiguration:From"];
        var to = configuration["MailConfiguration:To"];
        // Create a new email message
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(from);
        mailMessage.To.Add(to);
        mailMessage.Subject = subject;
        mailMessage.Body = body;

        smtpClient.Send(mailMessage);
    }
}