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
using System.Web.Helpers;
using System.Linq;


namespace FRONT.Controllers
{
  public class CatalogoController:Controller
    {

        private const string apiUrlList = "https://localhost:7023/Productos";
        private const string apiUrlactions = "https://localhost:7023/Producto?id_Producto={0}";
        private const string apiUrlImagenesList = "https://localhost:7023/Imagenes?id_producto={0}";

        private readonly ILogger<CatalogoController> _logger;

        public CatalogoController(ILogger<CatalogoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<EntityProducto> EntityProductoList = ListProductos();
            _logger.LogInformation($"Listado del catalogo de productos a las {DateTime.Now.ToLongTimeString()}");
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
            foreach(EntityProducto item in entityProductos)
            {
                item.imagenes = ListImagenes(item.ididentifier_i);
            }


            return entityProductos;
        }

        private static List<EntityImagen> ListImagenes(int id)
        {
            List<EntityImagen> entityImagenes = new List<EntityImagen>();

            string apiUrl = string.Format(apiUrlImagenesList, id);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                entityImagenes = JsonSerializer.Deserialize<List<EntityImagen>>(response.Content.ReadAsStringAsync().Result);
            }

            return entityImagenes;
        }

        // AGREGAR PRODUCTOS AL CARRITO
        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto)
        {


            var username_b = new Byte[40];
            var ip_b = new Byte[40];

            string ip; string user_name;

            if (HttpContext.Session.TryGetValue("UserName", out username_b))
            {
                user_name = System.Text.Encoding.UTF8.GetString(username_b);
            }
            
            if (HttpContext.Session.TryGetValue("IP", out ip_b))
            {
                ip = System.Text.Encoding.UTF8.GetString(ip_b);
            }

            // TODO: declarar instancia al carrito que es un GET de la conexion con carrito nuevo (si no lo hay)
            bool existe = false;
            //bool respuesta = false;
            string mensaje = string.Empty;
            if (existe)
            {
                // si el producto existe sumamos +1
                mensaje = "Hemos sumado la cantidad 1!";
            }
            else
            {
                // si no existe lo añadimos
                mensaje = "Hemos añadido el producto!";
            }

            return Json(new { respuesta = "respuesta", mensaje = mensaje });
        }
    }
}