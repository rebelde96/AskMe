using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMe.Data.Models;
using AskMe.Services;
using AskMe.Services.DTOs;
using AskMe.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AskMe.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserService userService;
		private readonly SignInManager<ApplicationUser> signInManager;

		public UserController(IUserService userService,
			SignInManager<ApplicationUser> signInManager)
		{
			this.userService = userService;
			this.signInManager = signInManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Register()
		{
			var model = new RegisterViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var userDto = new CreateUserDTO
				{
					Username = model.Username,
					Email = model.Email,
					Password = model.Password,
					ConfirmPassword = model.ConfirmPassword
				};
				var result = await this.userService.CreateUser(userDto);
				if (result.IsSuccessfull == false)
				{
					foreach (var error in result.ErrorMessages)
					{
						ModelState.AddModelError(error, error);
					}
					return this.View(model);
				}
				var loginViewModel = new LoginViewModel
				{
					Username = model.Username,
					Password = model.Password
				};
				return await this.Login(loginViewModel);
			}
			return this.View(model);
		}

		[HttpGet]
		public IActionResult Login()
		{
			var model = new LoginViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await this.signInManager.PasswordSignInAsync(model.Username,
				model.Password, model.RememberMe, false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "InsideHome");
				}
				else
				{
					ViewData["WrongDetails"] = "Wrong username or password!";
				}
			}
			return View(model);
		}

		public IActionResult ForgotPassword()
		{
			var model = new ForgotPasswordViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
			var userDto = new CreateUserDTO
			{
				Email = model.Email
			};
			var result = await userService.RecoverPassword(userDto);
			ViewData["Send"] = "Email send successfull!";
			return View(model);
		}
		
		public IActionResult ChangePassword(string id)
		{
			var isGuidValid = userService.CheckUserForgotenGuid(id);
			if (isGuidValid)
			{
				ViewData["isGuidValid"] = "true";			
			}
			var model = new ChangePasswordViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(string id, ChangePasswordViewModel model)
		{
			var result = await userService.CreateNewPassword(id, model.Password);
			return View(model);
		}
	}
}