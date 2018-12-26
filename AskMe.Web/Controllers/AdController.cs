using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMe.Data;
using AskMe.Services.Contracts;
using AskMe.Services.DTOs;
using AskMe.Services.Implementations;
using AskMe.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AskMe.Web.Controllers
{
	public class AdController : Controller
	{
		IAdService adService;
		ICategoryService categoryService;


		public AdController(IAdService adService,
			ICategoryService categoryService)
		{
			this.adService = adService;
			this.categoryService = categoryService;
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
			var model = new CreateAdViewModel { Categories = categories };
			return View(model);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateAd(CreateAdViewModel model)
		{
			var categories = await categoryService.GetAll();
			var createAdDTO = new CreateAdDTO
			{
				Title = model.Title,
				Description = model.Description,
				CategoryId = model.CategoryId
			};

			if (ModelState.IsValid)
			{
				var result = await adService.CreateAd(createAdDTO);
			}
			model.Title = string.Empty;
			model.Description = string.Empty;
			model.Categories = categories;
			return this.View(model);
		}

		public async Task<IActionResult> ViewDetailAd(int id)
		{
			var ad = await adService.GetAd(id);
			var model = new ViewDetailAdViewModel { Ad = ad };
			return View(model);
		}
	}
}
