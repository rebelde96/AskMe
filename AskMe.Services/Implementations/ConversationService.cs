using AskMe.Data;
using AskMe.Data.Models;
using AskMe.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<ConversationDTO> GetSingleConversation(string userId, int adId)
		{
			var appUserConversation = await GetIfConversationExist(userId, adId);
			var conversation = appUserConversation.Conversation;
			if (conversation != null)
			{
				var messages = new List<MessageDTO>();
				foreach (var message in conversation.Messages)
				{
					var messageDTO = new MessageDTO
					{
						Id = message.Id,
						MessageText = message.MessageText,
						ApplicationUserId = message.ApplicationUserId,
						ConversationId = message.ConversationId,
						CreatedAt = message.CreatedAt
					};
					messages.Add(messageDTO);
				}
				var userConversations = new List<ApplicationUserConversationDTO>();
				foreach (var userConversation in conversation.userConversations)
				{
					var userConversationsDTO = new ApplicationUserConversationDTO
					{
						ApplicationUserId = userConversation.ApplicationUserId,
						ConversationId = userConversation.ConversationId
					};
					userConversations.Add(userConversationsDTO);
				}
				var conversationDTO = new ConversationDTO
				{
					Id = conversation.Id,
					Messages = messages,
					userConversations = userConversations
				};
				return conversationDTO;
			}
			return null;
		}

		private async Task<ApplicationUserConversation> GetIfConversationExist(string userId, int adId)
		{
			var conversations = this.appContext.ApplicationUserConversations.Include(c => c.Conversation).Where(auc => auc.ApplicationUserId == userId && auc.Conversation.AdId == adId);
			if (conversations.Any())
			{
				return await conversations.FirstOrDefaultAsync();
			}
			return null;
		}

		public Task SendMessage(string content, int conversationId, int userId)
		{
			throw new NotImplementedException();
		}
	}
}
