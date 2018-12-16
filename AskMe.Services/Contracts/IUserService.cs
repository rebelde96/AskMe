using AskMe.Services.Common;
using AskMe.Services.DTOs;
using System.Threading.Tasks;

namespace AskMe.Services
{
	public interface IUserService
	{
		Task<OperationResult> CreateUser(CreateUserDTO dto);
	}
}
