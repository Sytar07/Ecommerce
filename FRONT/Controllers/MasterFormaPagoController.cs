using ECOMMERCE.CORE;
using FRONT.Code;
using FRONT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
    public class MasterFormasPagoController : Controller
    {

        private const string apiUrlList = "https://localhost:7023/FormasPago";
        private const string apiUrlactions = "https://localhost:7023/FormaPago?id_fpa={0}";

        private readonly ILogger<MasterFormasPagoController> _logger;

        public MasterFormasPagoController(ILogger<MasterFormasPagoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<EntityFormaPago> EntityFormaPagoList = ListFormaPagos();
            _logger.LogInformation($"Listado de formas de pago las {DateTime.Now.ToLongTimeString()}");
            return View(EntityFormaPagoList);
        }
        private static List<EntityFormaPago> ListFormaPagos()
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
            return View(FormaPago(id));
        }
        private static EntityFormaPago FormaPago(int id)
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
                _logger.LogInformation($"Grabación de {EntityFormaPago.ididentifier_i.ToString()} a las {DateTime.Now.ToLongTimeString()}");
                // Si es valido grabamos y salimos al Index.
                SaveFormaPago(EntityFormaPago).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View(EntityFormaPago);
        }
        private static async Task<EntityFormaPago> SaveFormaPago(EntityFormaPago EntityFormaPago)
        {
            string apiUrl = string.Format(apiUrlactions, EntityFormaPago.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl, EntityFormaPago);
            response.EnsureSuccessStatusCode();

            EntityFormaPago = await response.Content.ReadFromJsonAsync<EntityFormaPago>();

            return EntityFormaPago;
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(FormaPago(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EntityFormaPago EntityFormaPago)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Borrado de {EntityFormaPago.ididentifier_i.ToString()} a las {DateTime.Now.ToLongTimeString()}");
                // Si es valido grabamos y salimos al Index.
                DeleteFormaPago(EntityFormaPago).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit", EntityFormaPago);
        }
        private static async Task<EntityFormaPago> DeleteFormaPago(EntityFormaPago EntityFormaPago)
        {
            string apiUrl = string.Format(apiUrlactions, EntityFormaPago.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            EntityFormaPago = await response.Content.ReadFromJsonAsync<EntityFormaPago>();

            return EntityFormaPago;
        }

        [HttpGet]
        public IActionResult Create()
        {
            EntityFormaPago entityFormaPago = new EntityFormaPago();

            return View(entityFormaPago);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntityFormaPago EntityFormaPago)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Grabación de nuevo  {EntityFormaPago.name_nv} a las {DateTime.Now.ToLongTimeString()}");
                // Si es valido grabamos y salimos al Index.
                CreateFormaPago(EntityFormaPago).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit", EntityFormaPago);
        }
        private static async Task<EntityFormaPago> CreateFormaPago(EntityFormaPago EntityFormaPago)
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
            _logger.LogError($"Error: {Activity.Current?.Id ?? HttpContext.TraceIdentifier}");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}