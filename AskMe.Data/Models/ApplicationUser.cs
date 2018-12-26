using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class ApplicationUser : IdentityUser
	{
		public ApplicationUser()
		{
			this.UserInfo = new UserInfo();
			this.UserFiles = new List<UserFile>();
			this.Messages = new List<Message>();
			this.UserConversations = new List<ApplicationUserConversation>();
			this.ForgotenPasswords = new List<ForgotenPassword>();
			this.Ads = new List<Ad>();
			this.AdRatings = new List<AdRating>();
		}

		[Required]
		public DateTime CreatedAt { get; set; }

		public UserInfo UserInfo { get; set; }

		public ICollection<UserFile> UserFiles { get; set; }

		public ICollection<Message> Messages { get; set; }

		public ICollection<ApplicationUserConversation> UserConversations { get; set; }

		public ICollection<ForgotenPassword> ForgotenPasswords { get; set; }

		public ICollection<Ad> Ads { get; set; }

		public ICollection<AdRating> AdRatings { get; set; }
	}
}
