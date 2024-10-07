using Hotelier_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Hotelier_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly HotelDbContext context;


        public HomeController(HotelDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Permissions.ToList());
           
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
