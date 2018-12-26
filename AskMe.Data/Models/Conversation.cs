using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class Conversation
	{
		public Conversation()
		{
			this.Messages = new List<Message>();
			this.userConversations = new List<ApplicationUserConversation>();
		}

		public int Id { get; set; }

		public ICollection<Message> Messages { get; set; }

		public ICollection<ApplicationUserConversation> userConversations { get; set; }
	}
}
