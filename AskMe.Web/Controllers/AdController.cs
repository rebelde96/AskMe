using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMe.Data;
using AskMe.Data.Models;
using AskMe.Services.Contracts;
using AskMe.Services.DTOs;
using AskMe.Services.Implementations;
using AskMe.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AskMe.Web.Controllers
{
    public class AdController : Controller
    {
        IAdService adService;
        ICategoryService categoryService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public AdController(IAdService adService,
            ICategoryService categoryService,
            IHttpContextAccessor httpContextAccessor,
               UserManager<ApplicationUser> userManager,
                IMapper mapper)
        {
            this.adService = adService;
            this.categoryService = categoryService;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CreateAd()
        {
            var categories = await categoryService.GetAll();
            var model = new CreateAdViewModel();
            model.Categories = this.mapper.Map<List<CategoryViewModel>>(categories);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAd(CreateAdViewModel model)
        {
            var categories = await categoryService.GetAll();
            model.Categories = this.mapper.Map<List<CategoryViewModel>>(categories);
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
                var creatAdDto = this.mapper.Map<CreateAdDTO>(model);
                var result = await adService.CreateAd(creatAdDto, user.Id);
            }
            return this.View(model);
        }

        public async Task<IActionResult> ViewDetailAd(int id)
        {
            var ad = await adService.GetAd(id);
            var model = this.mapper.Map<ViewDetailAdViewModel>(ad);
            return View(model);
        }
    }
}
