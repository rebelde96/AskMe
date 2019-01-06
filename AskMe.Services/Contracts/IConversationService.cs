using AskMe.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.Services
{
    public interface IConversationService
    {
        Task<ConversationDTO> GetConversationByAdId(int adId, string userId, string adCreateUserId);

        Task SendMessage(string content, int conversationId, string userId, int adId);

        Task<ICollection<ConversationDTO>> GetUserConversations(string userId);

        string GetUserIdByAdId(int id);
    }
}
