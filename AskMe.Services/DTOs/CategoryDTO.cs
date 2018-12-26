using System;
using System.Collections.Generic;
using System.Text;

namespace AskMe.Services.DTOs
{
	public class CategoryDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
