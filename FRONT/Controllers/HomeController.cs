using ECOMMERCE.CORE;
using FRONT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FRONT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(EntityUser model)
        {
            if (ModelState.IsValid)
            {
                
                DAL.UsersBD usersBD = new DAL.UsersBD();
                List<EntityUser> Users = usersBD.GETALLUSERS();

                List<EntityUser> User = Users.Where(s => s.email_nv.ToUpper().Contains(model.email_nv.ToUpper())).ToList(); ;


                if (User.Count>0)
                {

                    if (User.First().Clave_nv == model.Clave_nv)
                    {
                        HttpContext.Session.SetString("UserName", model.email_nv);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}