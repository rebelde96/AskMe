﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class ForgotenPassword
	{
		public Guid Id { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public DateTime ExpireIn { get; set; }
	}
}
