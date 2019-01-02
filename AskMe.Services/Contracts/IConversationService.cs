using AskMe.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.Services
{
	public interface IConversationService
	{
		Task<ConversationDTO> GetSingleConversation(string userId, int adId);

		Task SendMessage(string content, int conversationId, int userId);
	}
}
