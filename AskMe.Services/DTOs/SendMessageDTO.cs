using System;
using System.Collections.Generic;
using System.Text;

namespace AskMe.Services.DTOs
{
    public class SendMessageDTO
    {
        public int Id { get; set; }

        public string MessageText { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
