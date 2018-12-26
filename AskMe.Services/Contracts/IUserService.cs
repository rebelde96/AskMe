using AskMe.Services.Common;
using AskMe.Services.DTOs;
using System.Threading.Tasks;

namespace AskMe.Services
{
	public interface IUserService
	{
		Task<OperationResult> CreateUser(CreateUserDTO dto);

		Task<OperationResult> RecoverPassword(string email);

		bool CheckUserForgotenGuid(string guid);

		Task<OperationResult> CreateNewPassword(string guid, string newPassword);

		Task<ApplicationUserDTO> GetUser(string guid);
	}
}
