using Inex_inter.Domain;
using Microsoft.AspNetCore.Mvc;
using Inex_inter.Service;

namespace Inex_inter.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;
            public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Index()
        {
            return View(dataManager.ServiceItems.GetServiceItems());
        }
    }
}
