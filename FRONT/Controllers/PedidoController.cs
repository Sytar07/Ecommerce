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
  public class PedidoController : Controller
    {

        private const string apiUrlList = "https://localhost:7023/FormasPago";
        private const string apiUrlDireccionesList = "https://localhost:7023/Direcciones?id_user={0}";
        private const string apiUrlConexion = "https://localhost:7023/Conexion?IP={0}&User={1}&conexion={2}";

        private readonly ILogger<PedidoController> _logger;

        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var conexion = new Byte[40];
            var username = new Byte[40];            
            var ip = new Byte[40];
            
            int id_conexion = 0;
            int user = 0;

            if (HttpContext.Session.TryGetValue("Conexion", out conexion))
            {
                id_conexion = int.Parse(System.Text.Encoding.UTF8.GetString(conexion));
            }
      

            _logger.LogInformation($"Tramitación y Pago del pedido a las {DateTime.Now.ToLongTimeString()}");
            List<EntityCarrito> carritolista = ListCarrito(id_conexion);
            return View("Index", carritolista);
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
        private static List<EntityFormaPago> ListFPA()
        {

            List<EntityFormaPago> formasdepago = new List<EntityFormaPago>();

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrlList).Result;
            if (response.IsSuccessStatusCode)
            {
                formasdepago = JsonSerializer.Deserialize<List<EntityFormaPago>>(response.Content.ReadAsStringAsync().Result);
            }
            

            return formasdepago;
        }

        
        public IActionResult Tramitar(int id)
        {
            EntityConexion conexion = GetConexion(id);
            EntityPedido Pedido = new EntityPedido();
            Pedido.FormaPagos = ListFPA();

            if (conexion.iduser != 0)
            {
                Pedido.Direcciones = ListDirecciones(conexion.iduser);
                return View("Direcciones", Pedido.Direcciones);
            }
            else
            {
                return View("Direccion", Pedido);
            }
        }

        private static List<EntityDireccion> ListDirecciones(int id)
        {

            List<EntityDireccion> entityDirecciones = new List<EntityDireccion>();
            string apiUrl = string.Format(apiUrlDireccionesList, id);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                entityDirecciones = JsonSerializer.Deserialize<List<EntityDireccion>>(response.Content.ReadAsStringAsync().Result);
            }

            return entityDirecciones;
        }

        private static EntityConexion GetConexion(int idconexion)
        {

            EntityConexion conexion = new EntityConexion();

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(string.Format(apiUrlConexion, "NA", "NONAME", idconexion)).Result;
            if (response.IsSuccessStatusCode)
            {
                conexion = JsonSerializer.Deserialize<EntityConexion>(response.Content.ReadAsStringAsync().Result);
            }

            return conexion;
        }

    }
}