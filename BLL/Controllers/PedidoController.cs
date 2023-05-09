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

namespace BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {

        private readonly ILogger<PedidoController> _logger;

        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// API: GetUser
        /// Espera un ID y resuelve con la entidad del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public EntityPedido Get(int id_Pedido)
        {
            PedidosBD Pedidos = new PedidosBD();
            return Pedidos.GETPedido(id_Pedido);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityPedido> Create(EntityPedido entityPedido)
        {
           PedidosBD Pedidos = new PedidosBD();
            
            var result= Pedidos.GETPedido(Pedidos.INSERTPedido(entityPedido));

            return CreatedAtAction(nameof(Get), new { id = entityPedido.ididentifier_i }, entityPedido);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityPedido> Update(EntityPedido entityPedido)
        {
            PedidosBD Pedidos = new PedidosBD();

            var result = Pedidos.GETPedido(Pedidos.UPDATEEstadoPedido(entityPedido));
            _logger.LogInformation($"API Create de Pedido {entityPedido.ididentifier_i} a las {DateTime.Now.ToLongTimeString()}");
            return CreatedAtAction(nameof(Get), new { id = entityPedido.ididentifier_i }, entityPedido);
        }

      

    }
}