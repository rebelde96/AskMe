using AskMe.Data;
using AskMe.Services.Contracts;
using AskMe.Services.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AskMe.Services.Implementations
{
	public class CategoryService : ICategoryService
	{
		private readonly ApplicationContext appContext;
        private readonly IMapper mapper;

        public CategoryService(ApplicationContext appContext,
            IMapper mapper)
		{
			this.appContext = appContext;
            this.mapper = mapper;
		}

		public async Task<ICollection<CategoryDTO>> GetAll()
		{
			var categories = await this.appContext.Categories.ToListAsync();
			var result = this.mapper.Map<List<CategoryDTO>>(categories);
            return result;
		}
	}
}
