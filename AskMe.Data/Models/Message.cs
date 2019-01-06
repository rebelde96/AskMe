using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class Message
	{
        public Message()
        {
            this.ApplicationUser = new ApplicationUser();
        }

		public int Id { get; set; }

		[Required]
		[MaxLength(4000)]
		public string MessageText { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public int ConversationId { get; set; }

		public Conversation Conversation { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }
	}
}
