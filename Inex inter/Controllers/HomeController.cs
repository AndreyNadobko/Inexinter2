using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Inex_inter.Controllers
{
    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

