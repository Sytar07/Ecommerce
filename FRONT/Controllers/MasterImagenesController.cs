using ECOMMERCE.CORE;
using FRONT.Code;
using FRONT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FRONT.Controllers
{
    [Authentication]
    public class MasterImagenesController : Controller
    {

        private const string apiUrlList = "https://localhost:7023/Imagenes?id_producto={0}";
        private const string apiUrlactions = "https://localhost:7023/Imagen?id_Imagen={0}";

        private readonly ILogger<MasterImagenesController> _logger;

        public MasterImagenesController(ILogger<MasterImagenesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            List<EntityImagen> EntityImagenList = ListImagenes(id);
            _logger.LogInformation($"Listado de imagenes de {id} las {DateTime.Now.ToLongTimeString()}");
            ViewBag.ID_PRODUCTO = id;
            return View(EntityImagenList);
        }
        private static List<EntityImagen> ListImagenes(int id)
        {
            List<EntityImagen> entityImagenes = new List<EntityImagen>();

            string apiUrl = string.Format(apiUrlList, id);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
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
                _logger.LogInformation($"Grabación de {entityImagen.ididentifier_i.ToString()} a las {DateTime.Now.ToLongTimeString()}");
                if (!(entityImagen.imagen == null || entityImagen.imagen.Length == 0))
                {
                    var rutaDeGuardado = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/productos", entityImagen.imagen.FileName);

                    using (var stream = new FileStream(rutaDeGuardado, FileMode.Create))
                    {
                        entityImagen.imagen.CopyTo(stream);
                    }
                    entityImagen.path_nv = entityImagen.imagen.FileName;// Sustiyo el nombre por el correcto
                }

                // Si es valido grabamos y salimos al Index.
                SaveImagen(entityImagen).Wait();
                return RedirectToAction("Index", new { id = entityImagen.id_producto });
            }
            // en los demás casos mostramos en pantalla
            return View(entityImagen);
        }
        private static async Task<EntityImagen> SaveImagen(EntityImagen entityImagen)
        {
            // Aqui solo recojo el nombre y borro el binario que ya lo tengo subido.
            
            entityImagen.imagen = null;


            string apiUrl = string.Format(apiUrlactions, entityImagen.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response =await client.PutAsJsonAsync(apiUrl,entityImagen);
            response.EnsureSuccessStatusCode();

            entityImagen = await response.Content.ReadFromJsonAsync<EntityImagen>();

            return entityImagen;
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View("Delete", Imagen(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EntityImagen entityImagen)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Borrado de {entityImagen.ididentifier_i.ToString()} a las {DateTime.Now.ToLongTimeString()}");
                // Si es valido grabamos y salimos al Index.
                DeleteImagen(entityImagen).Wait();
                return RedirectToAction("Index", new { id = entityImagen.id_producto });
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
        public IActionResult Create(int id)
        {
            EntityImagen entityImagen  =   new EntityImagen();
            entityImagen.id_producto= id;
            return View(entityImagen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntityImagen entityImagen)
        {
            if (ModelState.IsValid)
            {
                if (!(entityImagen.imagen == null || entityImagen.imagen.Length == 0))
                {
                    var rutaDeGuardado = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/productos", entityImagen.imagen.FileName);

                    using (var stream = new FileStream(rutaDeGuardado, FileMode.Create))
                    {
                        entityImagen.imagen.CopyTo(stream);
                    }
                    entityImagen.path_nv = entityImagen.imagen.FileName;// Sustiyo el nombre por el correcto
                }

                // Si es valido grabamos y salimos al Index.
                SaveImagen(entityImagen).Wait();

                _logger.LogInformation($"Grabación de nuevo  {entityImagen.path_nv} a las {DateTime.Now.ToLongTimeString()}");
                // Si es valido grabamos y salimos al Index.
                CreateImagen(entityImagen).Wait();
                return RedirectToAction("Index", new { id = entityImagen.id_producto});
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
            _logger.LogError($"Error: {Activity.Current?.Id ?? HttpContext.TraceIdentifier}");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}