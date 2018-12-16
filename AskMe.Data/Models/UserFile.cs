using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class UserFile
	{
		public int Id { get; set; }

		public string FileName { get; set; }

		public string FileLocation { get; set; }

		public DateTime CreatedAt { get; set; }

		public string ApplicationUserId { get; set; }

		public ApplicationUser User { get; set; }
	}
}
