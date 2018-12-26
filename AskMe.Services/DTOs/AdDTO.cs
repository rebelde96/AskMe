using AskMe.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskMe.Services.DTOs
{
	public class AdDTO
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public int CategoryId { get; set; }

		public CategoryDTO Category { get; set; }

		public DateTime CreatedAt { get; set; }

		public string ApplicationUserId { get; set; }

		public ApplicationUserDTO ApplicationUser { get; set; }

		public int ViewCount { get; set; }

		public double RatingPoints { get; set; }
	}
}
