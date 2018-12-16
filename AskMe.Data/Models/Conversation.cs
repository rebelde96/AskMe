using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class Conversation
	{
		public int Id { get; set; }

		public ICollection<Message> Messages { get; set; }

		public ICollection<ApplicationUserConversation> userConversations { get; set; }
	}
}
