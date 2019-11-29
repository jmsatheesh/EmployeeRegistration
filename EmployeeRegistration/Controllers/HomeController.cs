using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeRegistration.Models;
using Microsoft.Extensions.Options;

namespace EmployeeRegistration.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<ApplicatonSettings> _config;

        public HomeController(IOptions<ApplicatonSettings> config)
        {
            _config = config; 
        }
        public IActionResult Index()
        {
            
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
