using AskMe.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Models
{
	public class IndexViewModel
	{
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public CategoryDTO Category { get; set; }

        public DateTime CreatedAt { get; set; }

        public ApplicationUserDTO ApplicationUser { get; set; }

        public int ViewCount { get; set; }

        public double RatingPoints { get; set; }
    }
}
