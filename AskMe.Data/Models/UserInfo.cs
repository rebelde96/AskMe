using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class UserInfo
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public User User { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int Age { get; set; }
	}
}
