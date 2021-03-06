﻿using System.Collections.Generic;

namespace AskMe.Services.DTOs
{
	public class ConversationDTO
	{
		public int Id { get; set; }

        public AdDTO Ad { get; set; }

        public ICollection<MessageDTO> Messages { get; set; }

        public SendMessageDTO SendMessage { get; set; }
    }
}
