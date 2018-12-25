using AskMe.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.Services.Contracts
{
	public interface IEmailService
	{
		OperationResult SendEmail(string email, string body, string subject);
	}
}
