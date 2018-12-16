using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class ApplicationUser : IdentityUser
	{
		public DateTime CreatedAt { get; set; }

		public UserInfo UserInfo { get; set; }

		public ICollection<UserFile> UserFiles { get; set; }

		public ICollection<Message> Messages { get; set; }

		public ICollection<ApplicationUserConversation> userConversations { get; set; }
	}
}
