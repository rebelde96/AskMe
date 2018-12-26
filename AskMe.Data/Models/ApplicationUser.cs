﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class ApplicationUser : IdentityUser
	{
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
