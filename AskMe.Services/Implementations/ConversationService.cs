using AskMe.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.Services
{
	public class ConversationService : IConversationService
	{
		private readonly ApplicationContext appContext;

		public ConversationService(ApplicationContext appContext)
		{
			this.appContext = appContext;
		}

		public Task SendMessage(string content, int conversationId, int userId)
		{
			throw new NotImplementedException();
		}
	}
}
