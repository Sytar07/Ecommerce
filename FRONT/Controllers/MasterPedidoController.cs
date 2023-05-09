﻿using ECOMMERCE.CORE;
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
using System.Reflection;
using DAL;


namespace FRONT.Controllers
{
  public class MasterPedidoController : Controller
    {

        private const string apiUrlList = "https://localhost:7023/FormasPago";
        private const string apiUrlDireccionesList = "https://localhost:7023/Direcciones?id_user={0}";
        private const string apiUrlConexion = "https://localhost:7023/Conexion?IP={0}&User={1}&conexion={2}";
        private const string apiUrlPaises = "https://localhost:7023/Paises";
        private const string apiUrlDireccion = "https://localhost:7023/Direccion?id_Direccion={0}";
        private const string apiUrlPedido = "https://localhost:7023/Pedido";
        

        private readonly ILogger<MasterPedidoController> _logger;

        public MasterPedidoController(ILogger<MasterPedidoController> logger)
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
       
        public IActionResult Tramitar(int id)
        {
            EntityConexion conexion = GetConexion(id);
            EntityPedido Pedido = new EntityPedido();
            Pedido.FormaPagos = ListFPA();

            if (conexion.iduser != 0)
            {
                // Si es usuario registrado le mostramos sus direcciónes.
                Pedido.Direcciones = ListDirecciones(conexion.iduser);
                if (Pedido.Direcciones.Count > 0)
                {
                    ViewBag.id_user = conexion.iduser;
                    return View("Direcciones", Pedido.Direcciones);
                    
                }
                else
                {
                    // excepto si np  tiene ninguna que le pedimos que cree una nueva.
                    Pedido.Direccion.user_i = conexion.iduser;
                    
                    Pedido.Direccion.paises = DDLPaises();
                    return View("Direccion", Pedido);
                }
                
            }
            else
            {
                Pedido.Direccion = new EntityDireccion()
                {
                    user_i = 0
                };

                Pedido.Direccion.paises = DDLPaises();
                return View("Direccion", Pedido);
            }
        }

        
        public ActionResult SelecionarDireccion(int id, int userid)
        {            
            EntityDireccion direccion = new EntityDireccion();

            direccion = ListDirecciones(userid).Where(i => i.ididentifier_i==id).FirstOrDefault();
            direccion.paises = DDLPaises();
            
            return View("ConfirmarDireccion", direccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDireccion(EntityDireccion entityDireccion)
        {
            EntityTarjeta Tarjeta = new EntityTarjeta();
            entityDireccion.paises = new List<EntityPais>();

            if (ModelState.IsValid)
            {
                // Si es valido grabamos y vamos al pago.
                Tarjeta.NumeroTarjeta = "";
                Tarjeta.FechaVencimiento = DateTime.Now;
                Tarjeta.NombreTitular = "";
                Tarjeta.CVV = 0;
                // Los guardamos en el modelo para la sigueinte fase
                Tarjeta.iddireccion= entityDireccion.ididentifier_i;
                Tarjeta.iduser= entityDireccion.user_i;

                return View("RealizarPago", Tarjeta);
            }
            // en los demás casos mostramos en pantalla
            entityDireccion.paises = DDLPaises();
            return View("ConfirmarDireccion", entityDireccion);
        }
    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDireccion(EntityPedido entitypedido)
        {
            ModelState.Remove("Paises");
            EntityDireccion entityDireccion = entitypedido.Direccion;

            entityDireccion.paises = new List<EntityPais>();
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Grabación de nuevo  {entityDireccion.direccion_nv} a las {DateTime.Now.ToLongTimeString()} para un pedido");
                // Si es valido grabamos y vamos al pago.

                CrearDireccionPedido(entityDireccion).Wait();

                return View("Pago", entitypedido);
            }
            // en los demás casos mostramos en pantalla
            return View("CreateDireccion", entitypedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RealizarPago(EntityTarjeta entitytarjeta)
        {
         
            ModelState.Remove("Estado");
           
            if (ModelState.IsValid)
            {
                EntityPedido entityPedido = new EntityPedido();
                entityPedido.Tarjeta = entitytarjeta;
                entityPedido.id_direccion = entitytarjeta.iddireccion;
                entityPedido.id_user = entitytarjeta.iduser;
                ProcesarPedido(entityPedido).Wait();

                return View("PedidoPagado", entityPedido);
            }
           
            return View("RealizarPago", entitytarjeta);
        }

       

        private static async Task<EntityPedido> ProcesarPedido(EntityPedido Pedido)
        {
            string apiUrl = string.Format(apiUrlPedido);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, Pedido);
            response.EnsureSuccessStatusCode();

            Pedido = await response.Content.ReadFromJsonAsync<EntityPedido>();

            return Pedido;
        }

        private static async Task<EntityDireccion> CrearDireccionPedido(EntityDireccion entityDireccion)
        {
            string apiUrl = string.Format(apiUrlDireccion, entityDireccion.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, entityDireccion);
            response.EnsureSuccessStatusCode();

            entityDireccion = await response.Content.ReadFromJsonAsync<EntityDireccion>();

            return entityDireccion;
        }
        private static List<EntityPais> DDLPaises()
        {
            List<EntityPais> listadopaises = new List<EntityPais>();


            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrlPaises).Result;
            if (response.IsSuccessStatusCode)
            {
                listadopaises = JsonSerializer.Deserialize<List<EntityPais>>(response.Content.ReadAsStringAsync().Result);
            }

            return listadopaises;
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
    }
}