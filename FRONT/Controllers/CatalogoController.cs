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


        
        public IActionResult Carrito()
        {
            var conexion = new Byte[40];
            int id_conexion = 3;
       
            if (HttpContext.Session.TryGetValue("CONEXION", out conexion))
            {
                id_conexion =int.Parse(System.Text.Encoding.UTF8.GetString(conexion));
            }

            List<EntityCarrito> carritolista = ListCarrito(id_conexion);
            return PartialView("carrito", carritolista);
        }

        private static List<EntityCarrito> ListCarrito(int id)
        {
            List<EntityCarrito> entityCarrito = new List<EntityCarrito>();

            string apiUrl = string.Format("https://localhost:7023/Carrito?id_conexion={0}", id);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                entityCarrito = JsonSerializer.Deserialize<List<EntityCarrito>>(response.Content.ReadAsStringAsync().Result);
            }

            return entityCarrito;
        }


        [HttpPost]
        public IActionResult AgregarCarrito(int idproducto)
        {
            var conexion = new Byte[40];
            int id_conexion = 0;

            if (HttpContext.Session.TryGetValue("Conexion", out conexion))
            {
                id_conexion = int.Parse(System.Text.Encoding.UTF8.GetString(conexion));
            }

            InsertProducto(id_conexion, idproducto, 1).Wait();

            List<EntityCarrito> carritolista = ListCarrito(id_conexion);

            return PartialView("carrito",carritolista);
        }

        private static async Task<List<EntityCarrito>> InsertProducto(int conexion, int producto, int cantidad)
        {
            string apiUrl = string.Format("https://localhost:7023/Carrito");
           
            var myData = new agregarCarrito()
            {
                id_conexion = conexion,
                id_producto = producto,
                cantidad = cantidad
            };

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, myData);
            response.EnsureSuccessStatusCode();

            List<EntityCarrito> entityCarrito = await response.Content.ReadFromJsonAsync<List<EntityCarrito>>();

            return entityCarrito;
        }

       
    }
}