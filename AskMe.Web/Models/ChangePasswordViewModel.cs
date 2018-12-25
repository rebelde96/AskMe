using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Models
{
	public class ChangePasswordViewModel
	{
		[Required]
		public string Password { get; set; }

		[Required]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
