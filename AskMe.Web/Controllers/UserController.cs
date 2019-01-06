using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AskMe.Data.Models;
using AskMe.Services;
using AskMe.Services.DTOs;
using AskMe.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AskMe.Web.Controllers
{
    public class UserController : Controller
    {
        private IHostingEnvironment hostingEnvironment;
        private readonly IUserService userService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public UserController(IUserService userService,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment hostingEnvironment,
             IMapper mapper)
        {
            this.userService = userService;
            this.signInManager = signInManager;
            this.hostingEnvironment = hostingEnvironment;
            this.mapper = mapper;
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
                    return RedirectToAction("Index", "Home");
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
            var pathToFile = hostingEnvironment.WebRootPath
             + Path.DirectorySeparatorChar.ToString()
             + "Templates"
             + Path.DirectorySeparatorChar.ToString()
             + "EmailTemplate"
             + Path.DirectorySeparatorChar.ToString()
             + "ForgotPasswordTemplate.html";
            var result = await userService.RecoverPassword(model.Email, pathToFile);
            ViewData["Send"] = "Email send successfull!";
            return View(model);
        }

        public IActionResult ResetPassword(string id)
        {
            var isGuidValid = userService.CheckUserForgotenGuid(id);
            if (isGuidValid)
            {
                ViewData["isGuidValid"] = "true";
            }
            var model = new ResetPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string id, ResetPasswordViewModel model)
        {
            var result = await userService.CreateNewPassword(id, model.Password);
            return View(model);
        }

        public async Task<IActionResult> ViewUserProfile(string id)
        {
            var user = await userService.GetUser(id);
            var model = this.mapper.Map<UserProfileViewModel>(user);
            return View(model);
        }
    }
}