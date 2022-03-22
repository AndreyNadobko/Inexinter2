using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;



namespace Inex_inter.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHtmlLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }



            public IActionResult Index()
            {
                var test = _localizer["BookingYourHotel"];
                return View();
            }
            public IActionResult Privacy()
            {
                return View();
            }
            
    }
    }



