using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class Ad
	{
		public Ad()
		{
			this.AdRatings = new List<AdRating>();
		}

		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Required]
		[MaxLength(4000)]
		[MinLength(80)]
		public string Description { get; set; }

		public int CategoryId { get; set; }

		public Category Category { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }

		public string ApplicationUserId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int ViewCount { get; set; }

		public ICollection<AdRating> AdRatings { get; set; }
	}
}
