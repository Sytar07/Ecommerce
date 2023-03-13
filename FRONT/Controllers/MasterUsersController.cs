using ECOMMERCE.CORE;
using FRONT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace FRONT.Controllers
{
    public class MasterUserController : Controller
    {
        private readonly ILogger<MasterUserController> _logger;

        public MasterUserController(ILogger<MasterUserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<EntityUser> EntityUserList = ListUsers();            
            return View(EntityUserList);
        }
        private static List<EntityUser> ListUsers()
        {
            List<EntityUser> entityusers = new List<EntityUser>();
            string apiUrl = "https://localhost:44318/api/Users";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                entityusers = JsonSerializer.Deserialize<List<EntityUser>>(response.Content.ReadAsStringAsync().Result);
            }

            return entityusers;
        }


        public IActionResult Details(int id)
        {

            return View(new EntityUser()
            {
                ididentifier_i = 1,
                name_nv = "ESP"
            }
            );
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Vista y Modelo de ERROR. No tocar por ahora
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}