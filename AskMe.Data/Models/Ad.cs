﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class Ad
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public int CategoryId { get; set; }

		public Category Category { get; set; }

		public DateTime CreatedAt { get; set; }

		public string ApplicationUserId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		public int ViewCount { get; set; }

		public double RatingPoints { get; set; }
	}
}
