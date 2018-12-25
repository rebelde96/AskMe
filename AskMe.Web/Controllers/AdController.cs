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
	[Authorize]
	public class AdController : Controller
	{
		ApplicationContext appContext;
		IAdService adService;

		public AdController(ApplicationContext appContext,
			IAdService adService)
		{
			this.appContext = appContext;
			this.adService = adService;
		}

		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult CreateAd()
		{
			var categories = from c in appContext.Categories orderby c.Name select c;
			var model = new AddAdViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAd(AddAdViewModel model)
		{
			var createAdDTO = new CreateAdDTO
			{
				Title = model.Title,
				Description = model.Description,
				CategoryId = model.CategoryId
			};

			if (ModelState.IsValid)
			{
				var result = await adService.CreateAd(createAdDTO);
				return this.View();
			}
			return this.View(model);
		}
	}
}
