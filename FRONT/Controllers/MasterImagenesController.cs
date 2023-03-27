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
    public class MasterImagenesController : Controller
    {

        private const string apiUrlList = "https://localhost:7023/Imagenes";
        private const string apiUrlactions = "https://localhost:7023/Imagen?id_Imagen={0}";

        private readonly ILogger<MasterImagenesController> _logger;

        public MasterImagenesController(ILogger<MasterImagenesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<EntityImagen> EntityImagenList = ListImagenes();            
            return View(EntityImagenList);
        }
        private static List<EntityImagen> ListImagenes()
        {
            List<EntityImagen> entityImagenes = new List<EntityImagen>();
           

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrlList).Result;
            if (response.IsSuccessStatusCode)
            {
                entityImagenes = JsonSerializer.Deserialize<List<EntityImagen>>(response.Content.ReadAsStringAsync().Result);
            }

            return entityImagenes;
        }



        #region "Actions"

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(Imagen(id));
        }
        private static EntityImagen Imagen(int id)
        {
            EntityImagen entityImagen = new EntityImagen();
            string apiUrl = string.Format(apiUrlactions,id);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                entityImagen = JsonSerializer.Deserialize<EntityImagen>(response.Content.ReadAsStringAsync().Result);
            }

            return entityImagen;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntityImagen entityImagen)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                SaveImagen(entityImagen).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View(entityImagen);
        }
        private static async Task<EntityImagen> SaveImagen(EntityImagen entityImagen)
        {
            string apiUrl = string.Format(apiUrlactions, entityImagen.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response =await client.PutAsJsonAsync(apiUrl,entityImagen);
            response.EnsureSuccessStatusCode();

            entityImagen = await response.Content.ReadFromJsonAsync<EntityImagen>();

            return entityImagen;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EntityImagen entityImagen)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                DeleteImagen(entityImagen).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit",entityImagen);
        }
        private static async Task<EntityImagen> DeleteImagen(EntityImagen entityImagen)
        {
            string apiUrl = string.Format(apiUrlactions, entityImagen.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            entityImagen = await response.Content.ReadFromJsonAsync<EntityImagen>();

            return entityImagen;
        }

        [HttpGet]
        public IActionResult Create()
        {
            EntityImagen entityImagen  =   new EntityImagen();

            return View(entityImagen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntityImagen entityImagen)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                CreateImagen(entityImagen).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit", entityImagen);
        }

        private static async Task<EntityImagen> CreateImagen(EntityImagen entityImagen)
        {
            string apiUrl = string.Format(apiUrlactions, entityImagen.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl,entityImagen);
            response.EnsureSuccessStatusCode();

            entityImagen = await response.Content.ReadFromJsonAsync<EntityImagen>();

            return entityImagen;
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