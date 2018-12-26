using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class UserInfo
	{
		public int Id { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		public ApplicationUser User { get; set; }

		[MaxLength(100)]
		public string FirstName { get; set; }

		[MaxLength(100)]
		public string LastName { get; set; }

		[Range(1, 100)]
		public int Age { get; set; }
	}
}
