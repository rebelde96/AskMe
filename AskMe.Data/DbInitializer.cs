using AskMe.Data.Constants;
using AskMe.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Data
{
	public static class DbInitializer
	{
		public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
		{
			using (var context = new ApplicationContext(
				serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>()))
			{
				// For sample purposes seed both with the same password.
				// Password is set with the following:
				// dotnet user-secrets set SeedUserPW <pw>
				// The admin user can do anything

				var userID = await EnsureUser(serviceProvider, testUserPw, "user", "user@askme.com");
				await EnsureRole(serviceProvider, userID, Roles.AskMeUserRole);

				// allowed user can create and edit contacts that they create
				var adminID = await EnsureUser(serviceProvider, testUserPw, "admin", "admin@ask.me");
				await EnsureRole(serviceProvider, adminID, Roles.AskMeAdminRole);
			}
		}

		private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
													string testUserPw, string UserName, string Email)
		{
			var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

			var user = await userManager.FindByNameAsync(UserName);
			if (user == null)
			{
				user = new ApplicationUser { UserName = UserName, Email = Email };
				await userManager.CreateAsync(user, testUserPw);
			}

			return user.Id;
		}

		private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
															  string uid, string role)
		{
			IdentityResult IR = null;
			var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

			if (roleManager == null)
			{
				throw new Exception("roleManager null");
			}

			if (!await roleManager.RoleExistsAsync(role))
			{
				IR = await roleManager.CreateAsync(new IdentityRole(role));
			}

			var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

			var user = await userManager.FindByIdAsync(uid);

			IR = await userManager.AddToRoleAsync(user, role);

			return IR;
		}
	}
}
