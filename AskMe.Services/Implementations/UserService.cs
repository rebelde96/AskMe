using System;
using System.Linq;
using System.Threading.Tasks;
using AskMe.Data;
using AskMe.Data.Constants;
using AskMe.Data.Models;
using AskMe.Services.Common;
using AskMe.Services.DTOs;
using Microsoft.AspNetCore.Identity;

namespace AskMe.Services
{
	public class UserService : IUserService
	{
		private readonly ApplicationContext appContext;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public UserService(ApplicationContext appContext,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			this.appContext = appContext;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		public async Task<OperationResult> CreateUser(CreateUserDTO dto)
		{
			var operationResult = new OperationResult();
			var user = new ApplicationUser
			{
				Email = dto.Email,
				UserName = dto.Username,
				CreatedAt = DateTime.UtcNow
			};
			var result = await this.userManager.CreateAsync(user, dto.Password);
			if (result.Succeeded == false)
			{
				operationResult.IsSuccessfull = false;
				operationResult.ErrorMessages = result.Errors
					.Select(x => x.Description)
					.ToList<string>();
			}
			else
			{
				var identityResult = await userManager.AddToRoleAsync(user, Roles.AskMeUserRole);
				if (identityResult.Succeeded == false)
				{
					operationResult.IsSuccessfull = false;
					operationResult.ErrorMessages = identityResult.Errors
						.Select(x => x.Description)
						.ToList<string>();
				}
			}
			return operationResult;
		}
	}
}
