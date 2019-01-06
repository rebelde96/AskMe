using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AskMe.Data;
using AskMe.Data.Constants;
using AskMe.Data.Models;
using AskMe.Services.Common;
using AskMe.Services.Constants.EmailServiceConstants;
using AskMe.Services.Contracts;
using AskMe.Services.DTOs;
using AskMe.Services.Implementations;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AskMe.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext appContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmailService emailService;
        private readonly IMapper mapper;

        public UserService(ApplicationContext appContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            IMapper mapper)
        {
            this.appContext = appContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.emailService = emailService;
            this.mapper = mapper;
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

        public async Task<OperationResult> RecoverPassword(string email, string pathToFile)
        {
            var operationResult = new OperationResult();

            var user = await userManager.FindByEmailAsync(email);
            var forgotenPasswordGuid = "";
            if (user != null)
            {
                forgotenPasswordGuid = await GenerateForgotenPasswordGuid(user.Id);
            }


            var builder = new StringBuilder();
            using (StreamReader SourceReader = File.OpenText(pathToFile))
            {
                builder.Append(SourceReader.ReadToEnd());
            }
            var template = builder.ToString();
            template = template.Replace("${guid}", forgotenPasswordGuid);
            operationResult = emailService.SendEmail(email, template, EmailMessagesConstants.EMAIL_SUBJECT);
            return operationResult;
        }

        public async Task<string> GenerateForgotenPasswordGuid(string userID)
        {
            var forgotenPassword = new ForgotenPassword();
            forgotenPassword.ApplicationUserId = userID;
            forgotenPassword.Id = Guid.NewGuid();
            forgotenPassword.ExpireIn = DateTime.UtcNow.AddMinutes(15);
            await appContext.ForgotenPasswords.AddAsync(forgotenPassword);
            await appContext.SaveChangesAsync();
            var forgotenPasswordString = forgotenPassword.Id.ToString();
            return forgotenPasswordString;
        }

        public bool CheckUserForgotenGuid(string guid)
        {
            var isGuidValid = false;
            var userGuid = Guid.Parse(guid);
            var userForgotenPassword = from fp in appContext.ForgotenPasswords where fp.Id == userGuid select fp;
            if (userForgotenPassword.Any())
            {
                isGuidValid = userForgotenPassword.LastOrDefault().ExpireIn.CompareTo(DateTime.UtcNow) > 0;
            }
            return isGuidValid;
        }

        public async Task<OperationResult> CreateNewPassword(string guid, string newPassword)
        {
            var operationResult = new OperationResult();
            var userGuid = Guid.Parse(guid);
            var user = new ApplicationUser();
            var userForgotenPassword = from fp in appContext.ForgotenPasswords where fp.Id == userGuid select fp;
            if (userForgotenPassword.Any())
            {
                var forgotenPassword = userForgotenPassword.FirstOrDefault();
                user = (from a in appContext.Users where a.Id == forgotenPassword.ApplicationUserId select a).FirstOrDefault();
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, newPassword);
                var identityResult = await userManager.UpdateAsync(user);
                if (identityResult.Succeeded)
                {
                    operationResult.IsSuccessfull = true;
                    operationResult.SuccessMessages.Add("Password changed successfull!");
                }
                else
                {
                    operationResult.IsSuccessfull = false;
                    operationResult.SuccessMessages = identityResult.Errors
                        .Select(x => x.Description)
                        .ToList<string>();
                }
            }
            return operationResult;
        }

        public async Task<ApplicationUserDTO> GetUser(string userId)
        {
            var user = await GetIfUserExist(userId);

            if (user != null)
            {
                var applicationUserDTO = mapper.Map<ApplicationUserDTO>(user);
                return applicationUserDTO;
            }

            return null;
        }

        private async Task<ApplicationUser> GetIfUserExist(string userId)
        {
            var dbUsers = this.appContext.Users.Include(a => a.Ads).Include(ui => ui.UserInfo).Where(au => au.Id == userId);
            if (dbUsers.Any())
            {
                return await dbUsers.FirstOrDefaultAsync();
            }
            return null;
        }
    }
}
