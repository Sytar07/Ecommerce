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

        private const string apiUrlListFormasPago = "https://localhost:7023/FormasPago";
        private const string apiUrlLineasPedido = "https://localhost:7023/LPedido?id={0}";
        private const string apiUrlDireccionactions = "https://localhost:7023/Direccion?id_Direccion={0}";
        private const string apiUrlListImagenes = "https://localhost:7023/Imagenes?id_producto={0}";
        private const string apiUrlDireccionesList = "https://localhost:7023/Direcciones?id_user={0}";
        private const string apiUrlConexion = "https://localhost:7023/Conexion?IP={0}&User={1}&conexion={2}";
        private const string apiUrlPaises = "https://localhost:7023/Paises";
        private const string apiUrlDireccion = "https://localhost:7023/Direccion?id_Direccion={0}";

        private const string apiUrlPedido = "https://localhost:7023/Pedido";
        private const string apiUrlPedidos = "https://localhost:7023/Pedidos?id_user={0}";
        private const string apiUrlGetPedido = "https://localhost:7023/Pedido?id_pedido={0}";


        private readonly ILogger<MasterPedidoController> _logger;

        public MasterPedidoController(ILogger<MasterPedidoController> logger)
        {
            _logger = logger;
        }


        // PROCESO DE PEDIDO
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
            foreach(EntityCarrito entityCarrito in carritolista)
            {
                string imagenproducto = "/images/generic.png";
                string fototmp= ListImagenes(entityCarrito.idproducto)?.FirstOrDefault()?.path_nv;
                if (fototmp != null)
                {
                    imagenproducto = "/images/productos/" + fototmp;
                }
                
                entityCarrito.foto = imagenproducto;
            }
            return View("Index", carritolista);
        }
       
        public IActionResult Tramitar(int id)
        {
            EntityConexion conexion = GetConexion(id);
            EntityPedido Pedido = new EntityPedido();
            EntityDireccion Direccion = new EntityDireccion();
            List<EntityDireccion> Direcciones;

            if (conexion.iduser != 0)
            {
                // Si es usuario registrado le mostramos sus direcciónes.
                Direcciones = ListDirecciones(conexion.iduser);
                if (Direcciones.Count > 0)
                {
                    ViewBag.id_user = conexion.iduser;
                    return View("Direcciones", Direcciones);
                    
                }
                else
                {
                    // excepto si np  tiene ninguna que le pedimos que cree una nueva.
                    Direccion.user_i = conexion.iduser;
                    
                    Direccion.paises = DDLPaises();
                    return View("Direccion", Direccion);
                }
                
            }
            else
            {
                Direccion = new EntityDireccion()
                {
                    user_i = 0
                };

                Direccion.paises = DDLPaises();
                return View("Direccion", Direccion);
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

        [HttpGet]
        public IActionResult CrearDireccion(int id)
        {
            EntityDireccion direccion = new EntityDireccion();
            direccion.paises = DDLPaises();
            direccion.user_i = id;
            return View("Direccion",direccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearDireccion(EntityDireccion entityDireccion)
        {
            entityDireccion.paises = new List<EntityPais>();
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Grabación de nuevo  {entityDireccion.direccion_nv} a las {DateTime.Now.ToLongTimeString()} para un pedido");
                // Si es valido grabamos y vamos al pago.

                EntityDireccion nuevadireccion = CrearDireccionPedido(entityDireccion);

                EntityTarjeta Tarjeta = new EntityTarjeta();
                
                Tarjeta.NumeroTarjeta = "";
                Tarjeta.FechaVencimiento = DateTime.Now;
                Tarjeta.NombreTitular = "";
                Tarjeta.CVV = 0;
                // Los guardamos en el modelo para la sigueinte fase
                Tarjeta.iddireccion = nuevadireccion.ididentifier_i;
                Tarjeta.iduser = nuevadireccion.user_i;
                Tarjeta.id_fpa = 1; // Como solo hacemos pedidos por tarjheta lo dejamos fijo.
                return View("RealizarPago", Tarjeta);
            }
            // en los demás casos mostramos en pantalla
            return View("CreateDireccion", entityDireccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RealizarPago(EntityTarjeta entitytarjeta)
        {
         
            ModelState.Remove("Estado");
           
            if (ModelState.IsValid)
            {
                EntityPedido entityPedido = new EntityPedido();
                entityPedido.id_direccion = entitytarjeta.iddireccion;
                entityPedido.id_user = entitytarjeta.iduser;
                entityPedido.id_fpa = entitytarjeta.id_fpa;

                return View("PedidoPagado", entityPedido);
            }
           
            return View("RealizarPago", entitytarjeta);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RealizarPagoFase2(EntityPedido entityPedido)
        {

            ModelState.Remove("Estado");

            if (ModelState.IsValid)
            {
                var conexion = new Byte[40];
                int id_conexion = 0;


                if (HttpContext.Session.TryGetValue("Conexion", out conexion))
                {
                    id_conexion = int.Parse(System.Text.Encoding.UTF8.GetString(conexion));
                }
                entityPedido.conexion = id_conexion;

                EntityPedidoCompleto pedidoCompleto = ProcesarPedido(entityPedido);
                


                return View("VerPedido", pedidoCompleto);
            }

            return View("RealizarPago", entityPedido);
        }

        // END Proceso PEDIDO


        public IActionResult MisPedidos()
        {
            
            int id_user = 0;
            byte[]? UserId;
            if (HttpContext.Session.TryGetValue("UserId", out UserId))
            {
                id_user = int.Parse(System.Text.Encoding.UTF8.GetString(UserId));
            }
            

            _logger.LogInformation($"GET de los pedidos a las {DateTime.Now.ToLongTimeString()}");
            
            return View("Pedidos", GetPedidos(id_user));
        }

        public IActionResult TodosPedidos()
        {
            _logger.LogInformation($"GET de los TODOS pedidos a las {DateTime.Now.ToLongTimeString()}");

            return View("Pedidos", GetPedidos(0));
        }

        public IActionResult VerPedido(int id)
        {

            return View("VerPedido", GetPedidoCompleto(new EntityPedido() { ididentifier_i = id }));
        }
        public IActionResult CambiarEstadoPedido(int id)
        {

            return View("VerPedido", GetPedidoCompleto(EstadoPedido(new EntityPedido() { ididentifier_i = id })));
        }


        private static EntityPedidoCompleto ProcesarPedido(EntityPedido Pedido)
        {
            string apiUrl = string.Format(apiUrlPedido);
            EntityPedidoCompleto pedidoCompleto;
            List<EntityLineaPedido> entityLineaPedidos;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PostAsJsonAsync(apiUrl, Pedido).Result;
            response.EnsureSuccessStatusCode();

            Pedido = JsonSerializer.Deserialize<EntityPedido>(response.Content.ReadAsStringAsync().Result);
            

            return GetPedidoCompleto(Pedido);
        }
        private static List<EntityPedido> GetPedidos(int id_user)
        {
            string apiUrl = string.Format(apiUrlPedidos, id_user);
            List<EntityPedido> Pedidos;
            List<EntityLineaPedido> entityLineaPedidos;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            response.EnsureSuccessStatusCode();

            Pedidos = JsonSerializer.Deserialize<List<EntityPedido>>(response.Content.ReadAsStringAsync().Result);


            return Pedidos;
        }
        private static EntityPedido EstadoPedido(EntityPedido Pedido)
        {
            string apiUrl = string.Format(apiUrlGetPedido, Pedido.ididentifier_i);
            
            List<EntityLineaPedido> entityLineaPedidos;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PutAsJsonAsync(apiUrl, Pedido).Result;
            response.EnsureSuccessStatusCode();

            EntityPedido result_pedidoCompleto = JsonSerializer.Deserialize<EntityPedido>(response.Content.ReadAsStringAsync().Result);


            return result_pedidoCompleto;
        }
        private static EntityPedidoCompleto GetPedidoCompleto(EntityPedido Pedido)
        {
            string apiUrl = string.Format(apiUrlGetPedido, Pedido.ididentifier_i);
            EntityPedidoCompleto pedidoCompleto;
            List<EntityLineaPedido> entityLineaPedidos;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            response.EnsureSuccessStatusCode();

            pedidoCompleto = JsonSerializer.Deserialize<EntityPedidoCompleto>(response.Content.ReadAsStringAsync().Result);


            pedidoCompleto.direccionEntrega = Direccion(pedidoCompleto.id_direccion);
            pedidoCompleto.entityLineasPedido = LineasPedido(pedidoCompleto.ididentifier_i);

            return pedidoCompleto;
        }
        private static EntityDireccion Direccion(int id)
        {
            EntityDireccion entityDireccion = new EntityDireccion();
            string apiUrl = string.Format(apiUrlDireccionactions, id);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                entityDireccion = JsonSerializer.Deserialize<EntityDireccion>(response.Content.ReadAsStringAsync().Result);
                entityDireccion.paises = DDLPaises();
            }

            return entityDireccion;
        }
        private static List<EntityLineaPedido> LineasPedido(int pedido)
        {
            string apiUrl = string.Format(apiUrlLineasPedido,pedido);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<EntityLineaPedido>>(response.Content.ReadAsStringAsync().Result);

            
        }
        private static  EntityDireccion CrearDireccionPedido(EntityDireccion entityDireccion)
        {
            string apiUrl = string.Format(apiUrlDireccion, entityDireccion.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PostAsJsonAsync(apiUrl, entityDireccion).Result;
            response.EnsureSuccessStatusCode();

            entityDireccion = JsonSerializer.Deserialize<EntityDireccion>(response.Content.ReadAsStringAsync().Result);

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
        private static List<EntityDireccion> ListDirecciones(int? id)
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
        private static List<EntityImagen> ListImagenes(int id)
        {
            List<EntityImagen> entityImagenes = new List<EntityImagen>();

            string apiUrl = string.Format(apiUrlListImagenes, id);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                entityImagenes = JsonSerializer.Deserialize<List<EntityImagen>>(response.Content.ReadAsStringAsync().Result);
            }

            return entityImagenes;
        }
        private static List<EntityFormaPago> ListFPA()
        {

            List<EntityFormaPago> formasdepago = new List<EntityFormaPago>();

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrlListFormasPago).Result;
            if (response.IsSuccessStatusCode)
            {
                formasdepago = JsonSerializer.Deserialize<List<EntityFormaPago>>(response.Content.ReadAsStringAsync().Result);
            }


            return formasdepago;
        }
    }
}