using System;
using System.Collections.Generic;
using System.Text;

namespace AskMe.Services.DTOs
{
	public class CreateAdDTO
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public int CategoryId { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; }
	}
}
