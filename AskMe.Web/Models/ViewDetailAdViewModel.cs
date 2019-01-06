using AskMe.Services.DTOs;
using System;

namespace AskMe.Web.Models
{
    public class ViewDetailAdViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public CategoryDTO Category { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUserDTO ApplicationUser { get; set; }

        public int ViewCount { get; set; }

        public double RatingPoints { get; set; }
    }
}
