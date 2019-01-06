using AskMe.Services.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Models
{
	public class CreateAdViewModel
	{
		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Required]
		[MaxLength(4000)]
		[MinLength(80)]
		public string Description { get; set; }

		[Required]
		public int CategoryId { get; set; }

		public ICollection<CategoryViewModel> Categories { get; set; }
	}
}
