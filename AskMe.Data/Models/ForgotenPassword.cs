using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class ForgotenPassword
	{
		public Guid Id { get; set; }

		public string ApplicationUserId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }
		
		public DateTime ExpireIn { get; set; }
	}
}
