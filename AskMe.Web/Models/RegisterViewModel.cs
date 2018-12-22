using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Models
{
	public class RegisterViewModel
	{
		[Required]
		[RegularExpression("([A-Za-z0-9_\\-\\.]*)", 
			ErrorMessage = "Username can contains numbers, digits and symbols: ('-')('_')('.') only!")]
		[MinLength(3, ErrorMessage = "Username must be at least 3 symbols long!")]
		public string Username { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
