using System.Net;
using System.Net.Mail;

public class EMailMessage
{
    public string Subject {get; set;}
    public string Body {get; set;}
    public string Mailaddress = "servermonitoringbcp@gmail.com";
    public string recipientMail = "alina.winnemoeller@campus.kstl.de";

    public void SendEmail(string subject, string body)
    {
        Subject = subject;
        Body = body;



        // Create a new email message
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(Mailaddress);
        mailMessage.To.Add(recipientMail);
        mailMessage.Subject = Subject;
        mailMessage.Body = Body;

        // Send the email message
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.Credentials = new NetworkCredential("servermonitoringbcp@gmail.com", "bcpevpnauert");
        smtpClient.EnableSsl = true;
        smtpClient.Send(mailMessage);
    }
}
