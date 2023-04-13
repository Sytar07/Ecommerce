using ECOMMERCE.CORE;
using FRONT.Code;
using FRONT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FRONT.Controllers
{
    [Authentication]
    public class MasterUsersController : Controller
    {
        
        private const string apiUrlList = "https://localhost:7023/Users";
        private const string apiUrlactions = "https://localhost:7023/User?id_user={0}";

        private readonly ILogger<MasterUsersController> _logger;

        public MasterUsersController(ILogger<MasterUsersController> logger)
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
           

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrlList).Result;
            if (response.IsSuccessStatusCode)
            {
                entityusers = JsonSerializer.Deserialize<List<EntityUser>>(response.Content.ReadAsStringAsync().Result);
            }

            return entityusers;
        }



        #region "Actions"

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(User(id));
        }
        private static EntityUser User(int id)
        {
            EntityUser entityuser = new EntityUser();
            string apiUrl = string.Format(apiUrlactions,id);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                entityuser = JsonSerializer.Deserialize<EntityUser>(response.Content.ReadAsStringAsync().Result);
            }

            return entityuser;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntityUser entityUser)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                SaveUser(entityUser).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View(entityUser);
        }
        private static async Task<EntityUser> SaveUser(EntityUser entityUser)
        {
            string apiUrl = string.Format(apiUrlactions, entityUser.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response =await client.PutAsJsonAsync(apiUrl,entityUser);
            response.EnsureSuccessStatusCode();

            entityUser = await response.Content.ReadFromJsonAsync<EntityUser>();

            return entityUser;
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View("Delete",User(id));
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EntityUser entityUser)
        {
           
            // Si es valido grabamos y salimos al Index.
            DeleteUser(entityUser).Wait();
            return RedirectToAction("Index");
           
        }
        private static async Task<EntityUser> DeleteUser(EntityUser entityUser)
        {
            string apiUrl = string.Format(apiUrlactions, entityUser.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            entityUser = await response.Content.ReadFromJsonAsync<EntityUser>();

            return entityUser;
        }

        [HttpGet]
        public IActionResult Create()
        {
            EntityUser entityUser  =   new EntityUser();

            return View(entityUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntityUser entityUser)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                CreateUser(entityUser).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit", entityUser);
        }

        private static async Task<EntityUser> CreateUser(EntityUser entityUser)
        {
            string apiUrl = string.Format(apiUrlactions, entityUser.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl,entityUser);
            response.EnsureSuccessStatusCode();

            entityUser = await response.Content.ReadFromJsonAsync<EntityUser>();

            return entityUser;
        }
        #endregion

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