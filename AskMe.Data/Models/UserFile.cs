using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class UserFile
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(500)]
		public string FileName { get; set; }

		[Required]
		[MaxLength(500)]
		public string FileLocation { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		public ApplicationUser User { get; set; }
	}
}
