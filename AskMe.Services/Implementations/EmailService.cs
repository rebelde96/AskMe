using AskMe.Services.Common;
using AskMe.Services.Constants.EmailServiceConstants;
using AskMe.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.Services.Implementations
{
	public class EmailService : IEmailService
	{
		public OperationResult SendEmail(string email, string body, string subject)
		{
			var operationResult = new OperationResult();
			SmtpClient client = new SmtpClient("smtp.gmail.com");
			//client.UseDefaultCredentials = false;
			client.Credentials = new NetworkCredential("what.is.askme@gmail.com", "askme123321");
			client.Port = 587;
			client.EnableSsl = true;

			MailMessage mailMessage = new MailMessage();
			mailMessage.From = new MailAddress("askme@gmail.com");
			mailMessage.To.Add(email);
			mailMessage.Body = body;
			mailMessage.IsBodyHtml = true;
			mailMessage.Subject = subject;
			try
			{
				client.Send(mailMessage);
				operationResult.IsSuccessfull = true;
				operationResult.SuccessMessages.Add(EmailMessagesConstants.EMAIL_SEND_SUCCESSFULL);
			}
			catch (Exception ex)
			{
				operationResult.IsSuccessfull = false;
				operationResult.SuccessMessages.Add(EmailMessagesConstants.EMAIL_SEND_FAILD);
			}

			return operationResult;
		}
	}
}
