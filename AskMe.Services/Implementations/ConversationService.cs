using AskMe.Data;
using AskMe.Data.Models;
using AskMe.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMe.Services.Common;

namespace AskMe.Services
{
    public class ConversationService : IConversationService
    {
        private readonly ApplicationContext appContext;
   
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ConversationService(ApplicationContext appContext,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.appContext = appContext;
            this.userManager = userManager;       
            this.mapper = mapper;
        }

        public async Task<ConversationDTO> GetConversationByAdId(int adId, string userId, string adCreateUserId)
        {
            var appUserConversation = await GetIfConversationExist(adId, adCreateUserId);
            if (appUserConversation != null)
            {
                var conversation = appUserConversation.Conversation;               
                var conversationDTO = this.mapper.Map<ConversationDTO>(conversation);
                return conversationDTO;
            }
            //else
            //{
            //    var conversation = CreateConversation(adId, userId, adCreateUserId);
            //    var conversationDTO = mapper.Map<ConversationDTO>(conversation);
            //    return conversationDTO;
            //}
            return null;
        }

        private async Task<ApplicationUserConversation> GetIfConversationExist(int adId, string userId)
        {
            var conversations = this.appContext.ApplicationUserConversations
                .Include(c => c.Conversation)
                .Where(auc => auc.ApplicationUserId == userId && auc.Conversation.AdId == adId)
                .Include(m => m.Conversation.Messages)
                .ThenInclude(m => m.ApplicationUser);
            if (conversations.Any())
            {
                return await conversations.FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task SendMessage(string content, int conversationId, string userId, int adId)
        {
            if (conversationId == 0)
            {
                conversationId = await CreateConversation(adId, userId, GetUserIdByAdId(adId));
            }
            var message = new Message
            {
                MessageText = content,
                ConversationId = conversationId,
                ApplicationUserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await appContext.Messages.AddAsync(message);
            await appContext.SaveChangesAsync();
        }

        public async Task<ICollection<ConversationDTO>> GetUserConversations(string userId)
        {
            var conversations = await appContext.ApplicationUserConversations
                .Include(auc => auc.Conversation)
                .Include(auc => auc.ApplicationUser)
                .Where(auc => auc.ApplicationUserId == userId)
                .Select(auc => auc.Conversation)
                .Include(c => c.Ad)
                .ThenInclude(a => a.ApplicationUser)
                .Include(c => c.Messages)
                .ThenInclude(m => m.ApplicationUser)
                .ToListAsync();

            var mapped = this.mapper.Map<List<ConversationDTO>>(conversations);
            return mapped;
        }

        private async Task<int> CreateConversation(int adID,string userId, string adCreateUserId)
        {
            var conversation = new Conversation
            {
                AdId = adID,
                CreatedAt = DateTime.UtcNow
            };
            await appContext.Conversations.AddAsync(conversation);
            await appContext.SaveChangesAsync();
            var userConversation1 = new ApplicationUserConversation
            {
                ApplicationUserId = userId,
                ConversationId = conversation.Id
            };
            var userConversation2 = new ApplicationUserConversation
            {
                ApplicationUserId = adCreateUserId,
                ConversationId = conversation.Id
            };
            await appContext.ApplicationUserConversations.AddAsync(userConversation1);
            await appContext.ApplicationUserConversations.AddAsync(userConversation2);
            await appContext.SaveChangesAsync();
            return conversation.Id;
        }

        public string GetUserIdByAdId(int adId)
        {
            return appContext.Ads.Where(a => a.Id == adId).FirstOrDefault().ApplicationUserId;
        }
    }
}
