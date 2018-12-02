using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class User
	{
		public int Id { get; set; }

		public string Username { get; set; }

		public string Email { get; set; }

		public string PasswordHash { get; set; }

		public DateTime CreatedAt { get; set; }

		public bool IsEmailVerified { get; set; }

		public UserInfo UserInfo { get; set; }

		public ICollection<UserFile> UserFiles { get; set; }

		public ICollection<Message> Messages { get; set; }

		public ICollection<UserConversation> userConversations { get; set; }
	}
}
