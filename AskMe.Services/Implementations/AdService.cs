using AskMe.Data;
using AskMe.Data.Models;
using AskMe.Services.Common;
using AskMe.Services.Contracts;
using AskMe.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
				ViewCount = 0
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

		public async Task<AdDTO> GetAd(int adId)
		{
			var dbAd = await GetIfAdExist(adId);
			if (dbAd != null)
			{
				var rating = dbAd.AdRatings;
				var ratingPoints = 0.0;
				if (rating.Any())
				{
					ratingPoints = dbAd.AdRatings.Average(ar => ar.RatingPoints);
				}
				var adDTO = new AdDTO
				{
					Title = dbAd.Title,
					Description = dbAd.Description,
					Category = new CategoryDTO
					{
						Name = dbAd.Category.Name
					},
					ApplicationUser = new ApplicationUserDTO
					{
						ApplicationUserId = dbAd.ApplicationUser.Id,
						UserName = dbAd.ApplicationUser.UserName
					},
					RatingPoints = ratingPoints,
					ViewCount = dbAd.ViewCount
				};

				return adDTO;
			}
			return null;
		}

		public async Task<ICollection<AdDTO>> GetAll()
		{
			var dbResult = await this.appContext.Ads.Include(a => a.Category).ToListAsync();
			var result = new List<AdDTO>();

			foreach (var ad in dbResult)
			{
				var adDTO = new AdDTO
				{
					Id = ad.Id,
					Title = ad.Title,
					Description = ad.Description,
					Category = new CategoryDTO
					{
						Name = ad.Category.Name
					},
					CreatedAt = ad.CreatedAt,
					ApplicationUserId = ad.ApplicationUserId,
					ViewCount = ad.ViewCount
				};
				result.Add(adDTO);
			}
			return result;
		}

		private async Task<Ad> GetIfAdExist(int adId)
		{
			var dbAds = this.appContext.Ads.Where(a => a.Id == adId).Include(c => c.Category).Include(ar => ar.AdRatings).Include(au => au.ApplicationUser);
			if (dbAds.Any())
			{
				return await dbAds.FirstOrDefaultAsync();
			}
			return null;
		}
	}
}
