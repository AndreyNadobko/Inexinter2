using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Inex_inter.Controllers
{
    public class AboutusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
