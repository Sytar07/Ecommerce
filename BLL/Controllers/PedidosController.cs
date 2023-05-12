using Microsoft.AspNetCore.Mvc;
using ECOMMERCE.CORE;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {

        private readonly ILogger<PedidoController> _logger;

        public PedidosController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// API: GetUser
        /// Espera un ID y resuelve con la entidad del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public List<EntityPedido> Get(int id_user)
        {
            PedidosBD Pedidos = new PedidosBD();
            if (id_user > 0)
            {
                return Pedidos.GETALLPedidos().Where(i => i.id_user == id_user).ToList();
            }else
            { 
                return Pedidos.GETALLPedidos().ToList(); 
            }
        }

       

    }
}