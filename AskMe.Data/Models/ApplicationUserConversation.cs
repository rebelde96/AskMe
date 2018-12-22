﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class ApplicationUserConversation
	{
		public string ApplicationUserId { get; set; }

		public ApplicationUser User { get; set; }

		public int ConversationId { get; set; }

		public Conversation Conversation { get; set; }
	}
}