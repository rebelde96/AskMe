using AskMe.Data;
using AskMe.Data.Models;
using AskMe.Services.Common;
using AskMe.Services.Contracts;
using AskMe.Services.DTOs;
using AutoMapper;
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
        private readonly IMapper mapper;

        public AdService(ApplicationContext appContext,
            IMapper mapper)
        {
            this.appContext = appContext;
            this.mapper = mapper;
        }

        public async Task<OperationResult> CreateAd(CreateAdDTO createAdDTO, string userId)
        {
            var operatonResult = new OperationResult();
            var ad = new Ad
            {
                Title = createAdDTO.Title,
                Description = createAdDTO.Description,
                CategoryId = createAdDTO.CategoryId,
                ApplicationUserId = userId,
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
                var adDTO = this.mapper.Map<AdDTO>(dbAd);
                return adDTO;
            }
            return null;
        }

        public async Task<ICollection<AdDTO>> GetAll()
        {
            var ads = await this.appContext.Ads.Include(a => a.Category).ToListAsync();
            var result = this.mapper.Map<List<AdDTO>>(ads);
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
