using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AskMe.Web.Models;
using Microsoft.AspNetCore.Authorization;
using AskMe.Services.Contracts;
using AutoMapper;

namespace AskMe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdService adService;
        private readonly IMapper mapper;

        public HomeController(IAdService adService,
            IMapper mapper)
        {
            this.adService = adService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var adDTOs = await this.adService.GetAll();
            var viewModel = this.mapper.Map<List<IndexViewModel>>(adDTOs);
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
