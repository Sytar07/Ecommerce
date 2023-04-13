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
    public class MasterProductosController : Controller
    {

        private const string apiUrlList = "https://localhost:7023/Productos";
        private const string apiUrlactions = "https://localhost:7023/Producto?id_Producto={0}";

        private readonly ILogger<MasterProductosController> _logger;

        public MasterProductosController(ILogger<MasterProductosController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<EntityProducto> EntityProductoList = ListProductos();            
            return View(EntityProductoList);
        }
        private static List<EntityProducto> ListProductos()
        {
            List<EntityProducto> entityProductos = new List<EntityProducto>();
           

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrlList).Result;
            if (response.IsSuccessStatusCode)
            {
                entityProductos = JsonSerializer.Deserialize<List<EntityProducto>>(response.Content.ReadAsStringAsync().Result);
            }

            return entityProductos;
        }



        #region "Actions"

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(Producto(id));
        }
        private static EntityProducto Producto(int id)
        {
            EntityProducto entityProducto = new EntityProducto();
            string apiUrl = string.Format(apiUrlactions,id);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                entityProducto = JsonSerializer.Deserialize<EntityProducto>(response.Content.ReadAsStringAsync().Result);
            }

            return entityProducto;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntityProducto entityProducto)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                SaveProducto(entityProducto).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View(entityProducto);
        }
        private static async Task<EntityProducto> SaveProducto(EntityProducto entityProducto)
        {
            string apiUrl = string.Format(apiUrlactions, entityProducto.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response =await client.PutAsJsonAsync(apiUrl,entityProducto);
            response.EnsureSuccessStatusCode();

            entityProducto = await response.Content.ReadFromJsonAsync<EntityProducto>();

            return entityProducto;
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View("Delete",Producto(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EntityProducto entityProducto)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                DeleteProducto(entityProducto).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit",entityProducto);
        }
        private static async Task<EntityProducto> DeleteProducto(EntityProducto entityProducto)
        {
            string apiUrl = string.Format(apiUrlactions, entityProducto.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            entityProducto = await response.Content.ReadFromJsonAsync<EntityProducto>();

            return entityProducto;
        }

        [HttpGet]
        public IActionResult Create()
        {
            EntityProducto entityProducto  =   new EntityProducto();

            return View(entityProducto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntityProducto entityProducto)
        {
            if (ModelState.IsValid)
            {
                // Si es valido grabamos y salimos al Index.
                CreateProducto(entityProducto).Wait();
                return RedirectToAction("Index");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit", entityProducto);
        }

        private static async Task<EntityProducto> CreateProducto(EntityProducto entityProducto)
        {
            string apiUrl = string.Format(apiUrlactions, entityProducto.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl,entityProducto);
            response.EnsureSuccessStatusCode();

            entityProducto = await response.Content.ReadFromJsonAsync<EntityProducto>();

            return entityProducto;
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