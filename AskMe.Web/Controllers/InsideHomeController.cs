using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Controllers
{
	[Authorize]
	public class InsideHomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
