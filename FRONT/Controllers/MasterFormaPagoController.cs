using ECOMMERCE.CORE;
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
    public class MasterFormasPagoController : Controller
    {

        private const string apiUrlList = "https://localhost:7023/FormasPago";
        private const string apiUrlactions = "https://localhost:7023/FormasPago?id={0}";

        private readonly ILogger<MasterFormasPagoController> _logger;

        public MasterFormasPagoController(ILogger<MasterFormasPagoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<EntityFormaPago> EntityFormaPagoList = ListUsers();
            return View(EntityFormaPagoList);
        }
        private static List<EntityFormaPago> ListUsers()
        {
            List<EntityFormaPago> EntityFormaPagos = new List<EntityFormaPago>();


            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrlList).Result;
            if (response.IsSuccessStatusCode)
            {
                EntityFormaPagos = JsonSerializer.Deserialize<List<EntityFormaPago>>(response.Content.ReadAsStringAsync().Result);
            }

            return EntityFormaPagos;
        }



        #region "Actions"

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(User(id));
        }
        private static EntityFormaPago User(int id)
        {
            EntityFormaPago EntityFormaPago = new EntityFormaPago();
            string apiUrl = string.Format(apiUrlactions, id);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                EntityFormaPago = JsonSerializer.Deserialize<EntityFormaPago>(response.Content.ReadAsStringAsync().Result);
            }

            return EntityFormaPago;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntityFormaPago EntityFormaPago)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                SaveUser(EntityFormaPago).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View(EntityFormaPago);
        }
        private static async Task<EntityFormaPago> SaveUser(EntityFormaPago EntityFormaPago)
        {
            string apiUrl = string.Format(apiUrlactions, EntityFormaPago.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl, EntityFormaPago);
            response.EnsureSuccessStatusCode();

            EntityFormaPago = await response.Content.ReadFromJsonAsync<EntityFormaPago>();

            return EntityFormaPago;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EntityFormaPago EntityFormaPago)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                DeleteUser(EntityFormaPago).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit", EntityFormaPago);
        }
        private static async Task<EntityFormaPago> DeleteUser(EntityFormaPago EntityFormaPago)
        {
            string apiUrl = string.Format(apiUrlactions, EntityFormaPago.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            EntityFormaPago = await response.Content.ReadFromJsonAsync<EntityFormaPago>();

            return EntityFormaPago;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntityFormaPago EntityFormaPago)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                CreateUser(EntityFormaPago).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit", EntityFormaPago);
        }
        private static async Task<EntityFormaPago> CreateUser(EntityFormaPago EntityFormaPago)
        {
            string apiUrl = string.Format(apiUrlactions, EntityFormaPago.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, EntityFormaPago);
            response.EnsureSuccessStatusCode();

            EntityFormaPago = await response.Content.ReadFromJsonAsync<EntityFormaPago>();

            return EntityFormaPago;
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