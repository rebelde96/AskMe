using AskMe.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AskMe.Services.Contracts
{
	public interface ICategoryService
	{
		Task<ICollection<CategoryDTO>> GetAll();
	}
}
