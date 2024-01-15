using System.Net.Mail;
using System.Net;

namespace FE.Helper.Utils;

public static class MailSender {
	public const string SENDERMAIL = "fatmapiksel@outlook.com";
	public const string SENDERPASSWORD = "ps<uH@W4k39O";
	public static void Send(IEnumerable<string> mailAddresses, string title,
		string message) {
		SmtpClient client = new SmtpClient();
		client.Port = 587;
		client.Host = "smtp-mail.outlook.com";
		client.EnableSsl = true;
		client.Timeout = 50000;

		string senderMail = SENDERMAIL;
		string senderPassword = SENDERPASSWORD;
		client.Credentials = new NetworkCredential(senderMail, senderPassword);

		MailMessage mail = new MailMessage();

		mail.From = new MailAddress(senderMail, "Fatma Erkan");

		foreach (string mailAddress in mailAddresses) {
			mail.To.Add(mailAddress);
		}

		mail.Subject = title;
		mail.IsBodyHtml = true;
		mail.Body = message;

		client.Send(mail);
	}

	public static void SendAdmin(string title, string message) {
		SmtpClient client = new SmtpClient();
		client.Port = 587;
		client.Host = "smtp-mail.outlook.com";
		client.EnableSsl = true;
		client.Timeout = 50000;

		string senderMail = SENDERMAIL;
		string senderPassword = SENDERPASSWORD;
		client.Credentials = new NetworkCredential(senderMail, senderPassword);

		MailMessage mail = new MailMessage();

		mail.From = new MailAddress(senderMail, "Fatma Erkan");
		mail.To.Add("erkann.fatma@gmail.com");

		mail.Subject = title;
		mail.IsBodyHtml = true;
		mail.Body = message;

		client.Send(mail);
	}
}
