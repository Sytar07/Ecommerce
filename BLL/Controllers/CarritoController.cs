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
using System.Linq;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarritoController : ControllerBase
    {
        
        [HttpGet(Name = "GetCarrito")]
        public List<EntityCarrito> Get(int id_conexion)
        {
            CarritoBD carrito = new CarritoBD();
            return carrito.GETCARRITO(id_conexion).ToList();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<EntityCarrito>> Create(agregarCarrito agregarCarrito)
        {
            CarritoBD carrito = new CarritoBD();
            if (carrito.INSERT_PRODUCTO(agregarCarrito.id_conexion, agregarCarrito.id_producto, agregarCarrito.cantidad) > 0)
            {
                // 
            }
            var carrito_list = carrito.GETCARRITO(agregarCarrito.id_conexion).ToList();
            return new OkObjectResult(carrito_list);
            //return CreatedAtAction(nameof(Get), carrito_list);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<EntityCarrito>> Update(agregarCarrito agregarCarrito)
        {
            CarritoBD carrito = new CarritoBD();
            if (carrito.DELETE_PRODUCTO(agregarCarrito.id_conexion, agregarCarrito.id_producto, agregarCarrito.cantidad) > 0)
            {
                // 
            }
            var carrito_list = carrito.GETCARRITO(agregarCarrito.id_conexion).ToList();
            return new OkObjectResult(carrito_list);
            //return CreatedAtAction(nameof(Get), carrito_list);
        }



    }
}