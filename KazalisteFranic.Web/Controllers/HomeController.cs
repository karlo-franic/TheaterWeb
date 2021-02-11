using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KazalisteFranic.Web.Models;
using KazalisteFranic.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using KazalisteFranic.Model;
using Microsoft.EntityFrameworkCore;

namespace KazalisteFranic.Web.Controllers
{
    public class HomeController : Controller
    {
        private KazalisteManagerDbContext _dbContext;

        public HomeController(KazalisteManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {                 
            return View();
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
