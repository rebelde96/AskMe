using AskMe.Data;
using AskMe.Services.Contracts;
using AskMe.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AskMe.Services.Implementations
{
	public class CategoryService : ICategoryService
	{
		private readonly ApplicationContext appContext;

		public CategoryService(ApplicationContext appContext)
		{
			this.appContext = appContext;
		}

		public async Task<ICollection<CategoryDTO>> GetAll()
		{
			var dbResult = await this.appContext.Categories.ToListAsync();
			var result = new List<CategoryDTO>();

			foreach (var category in dbResult)
			{
				var categoryDTO = new CategoryDTO
				{
					Id = category.Id,
					Name = category.Name,
					CreatedAt = category.CreatedAt
				};

				result.Add(categoryDTO);
			}
			return result;
		}
	}
}
