using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class AdRating
	{
		public int Id { get; set; }

		public int AdId { get; set; }

		public Ad Ad { get; set; }

		public string ApplicationUserId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		[Range(0, 10)]
		public int RatingPoints { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }
	}
}
