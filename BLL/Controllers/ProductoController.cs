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
    public class ProductoController : ControllerBase
    {

        private readonly ILogger<ProductoController> _logger;

        public ProductoController(ILogger<ProductoController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// API: GetUser
        /// Espera un ID y resuelve con la entidad del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetProducto")]
        public EntityProducto Get(int id_producto)
        {
            ProductosBD productos = new ProductosBD();
            return productos.GETPRODUCTO(id_producto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityProducto> Create(EntityProducto entityProducto)
        {
           ProductosBD productos = new ProductosBD();
            
            var result= productos.GETPRODUCTO(productos.INSERTPRODUCTO(entityProducto));

            return CreatedAtAction(nameof(Get), new { id = entityProducto.ididentifier_i }, entityProducto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityProducto> Update(EntityProducto entityProducto)
        {
            ProductosBD productos = new ProductosBD();

            var result = productos.GETPRODUCTO(productos.UPDATEPRODUCTO(entityProducto));
            _logger.LogInformation($"API Create de producto {entityProducto.nombre_nv} las {DateTime.Now.ToLongTimeString()}");
            return CreatedAtAction(nameof(Get), new { id = entityProducto.ididentifier_i }, entityProducto);
        }


       
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityProducto> Delete(int id_Producto)
        {
            ProductosBD productos = new ProductosBD();
            EntityProducto entityProducto = productos.GETPRODUCTO(id_Producto);
            productos.DELETEPRODUCTO(entityProducto);
            
            return CreatedAtAction(nameof(Get), new { id = entityProducto.ididentifier_i }, entityProducto);
        }

    }
}