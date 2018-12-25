using AskMe.Data;
using AskMe.Data.Models;
using AskMe.Services.Common;
using AskMe.Services.Contracts;
using AskMe.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.Services.Implementations
{
	public class AdService : IAdService
	{
		private readonly ApplicationContext appContext;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IHttpContextAccessor httpContextAccessor;

		public AdService(ApplicationContext appContext,
			UserManager<ApplicationUser> userManager,
			IHttpContextAccessor httpContextAccessor)
		{
			this.appContext = appContext;
			this.userManager = userManager;
			this.httpContextAccessor = httpContextAccessor;
		}

		public async Task<OperationResult> CreateAd(CreateAdDTO createAdDTO)
		{
			var operatonResult = new OperationResult();
			var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
			var ad = new Ad
			{
				Title = createAdDTO.Title,
				Description = createAdDTO.Description,
				CategoryId = createAdDTO.CategoryId,
				ApplicationUserId = user.Id,
				CreatedAt = DateTime.UtcNow,
				ViewCount = 0,
				RatingPoints = 0
			};
			try
			{
				await appContext.Ads.AddAsync(ad);
				await appContext.SaveChangesAsync();
				operatonResult.IsSuccessfull = true;
				operatonResult.SuccessMessages.Add("Ad created successfully");
			}
			catch (Exception ex)
			{
				operatonResult.IsSuccessfull = false;
				operatonResult.ErrorMessages.Add("Something went wrong!");
			}
			return operatonResult;
		}
	}
}
