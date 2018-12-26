﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data.Models
{
	public class Category
	{
		public Category()
		{
			this.Ads = new List<Ad>();
		}

		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }

		public ICollection<Ad> Ads { get; set; }
	}
}
