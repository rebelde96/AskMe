using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Models
{
	public class AddAdViewModel
	{
		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Required]
		[MaxLength(4000)]
		public string Description { get; set; }

		public int CategoryId { get; set; }
	}
}
