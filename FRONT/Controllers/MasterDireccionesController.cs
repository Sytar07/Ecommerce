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
    public class MasterDireccionesController : Controller
    {

        private const string apiUrlList = "https://localhost:7023/Direcciones";
        private const string apiUrlactions = "https://localhost:7023/Direccion?id_Direccion={0}";

        private readonly ILogger<MasterDireccionesController> _logger;

        public MasterDireccionesController(ILogger<MasterDireccionesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<EntityDireccion> EntityDireccionList = ListDirecciones();            
            return View(EntityDireccionList);
        }
        private static List<EntityDireccion> ListDirecciones()
        {
            List<EntityDireccion> entityDirecciones = new List<EntityDireccion>();
           

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrlList).Result;
            if (response.IsSuccessStatusCode)
            {
                entityDirecciones = JsonSerializer.Deserialize<List<EntityDireccion>>(response.Content.ReadAsStringAsync().Result);
            }

            return entityDirecciones;
        }



        #region "Actions"

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(Direccion(id));
        }
        private static EntityDireccion Direccion(int id)
        {
            EntityDireccion entityDireccion = new EntityDireccion();
            string apiUrl = string.Format(apiUrlactions,id);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                entityDireccion = JsonSerializer.Deserialize<EntityDireccion>(response.Content.ReadAsStringAsync().Result);
            }

            return entityDireccion;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntityDireccion entityDireccion)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                SaveDireccion(entityDireccion).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View(entityDireccion);
        }
        private static async Task<EntityDireccion> SaveDireccion(EntityDireccion entityDireccion)
        {
            string apiUrl = string.Format(apiUrlactions, entityDireccion.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response =await client.PutAsJsonAsync(apiUrl,entityDireccion);
            response.EnsureSuccessStatusCode();

            entityDireccion = await response.Content.ReadFromJsonAsync<EntityDireccion>();

            return entityDireccion;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EntityDireccion entityDireccion)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                DeleteDireccion(entityDireccion).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit",entityDireccion);
        }
        private static async Task<EntityDireccion> DeleteDireccion(EntityDireccion entityDireccion)
        {
            string apiUrl = string.Format(apiUrlactions, entityDireccion.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            entityDireccion = await response.Content.ReadFromJsonAsync<EntityDireccion>();

            return entityDireccion;
        }

        [HttpGet]
        public IActionResult Create()
        {
            EntityDireccion entityDireccion  =   new EntityDireccion();

            return View(entityDireccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntityDireccion entityDireccion)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                CreateDireccion(entityDireccion).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit", entityDireccion);
        }

        private static async Task<EntityDireccion> CreateDireccion(EntityDireccion entityDireccion)
        {
            string apiUrl = string.Format(apiUrlactions, entityDireccion.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl,entityDireccion);
            response.EnsureSuccessStatusCode();

            entityDireccion = await response.Content.ReadFromJsonAsync<EntityDireccion>();

            return entityDireccion;
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