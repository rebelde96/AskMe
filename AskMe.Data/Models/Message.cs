using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class Message
	{
		public int Id { get; set; }

		public string MessageText { get; set; }

		public int UserId { get; set; }

		public User User { get; set; }

		public int ConversationId { get; set; }

		public Conversation Conversation { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
