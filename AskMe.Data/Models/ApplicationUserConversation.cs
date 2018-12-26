using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class ApplicationUserConversation
	{
		[Required]
		public string ApplicationUserId { get; set; }

		public ApplicationUser User { get; set; }

		[Required]
		public int ConversationId { get; set; }

		public Conversation Conversation { get; set; }
	}
}
