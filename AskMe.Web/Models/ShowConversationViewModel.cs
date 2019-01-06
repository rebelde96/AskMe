using AskMe.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Models
{
    public class ShowConversationViewModel
    {
        public int Id { get; set; }

        public ViewDetailAdViewModel Ad { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }

        public SendMessageViewModel SendMessage { get; set; }
    }
}
