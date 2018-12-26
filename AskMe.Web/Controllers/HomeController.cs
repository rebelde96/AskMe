using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AskMe.Web.Models;
using Microsoft.AspNetCore.Authorization;
using AskMe.Services.Contracts;

namespace AskMe.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IAdService adService;

		public HomeController(IAdService adService)
		{
			this.adService = adService;
		}

		public async Task<IActionResult> Index()
		{
			var serviceResult = await this.adService.GetAll();
			var viewModel = new IndexViewModel
			{
				Ads = serviceResult
			};
			return View(viewModel);
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
