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

namespace BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarritoController : ControllerBase
    {
        // TODO: revisar carrito

        ///// <summary>
        ///// API: GetUser
        ///// Espera un ID y resuelve con la entidad del usuario
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet(Name = "GetCarrito")]
        //public EntityCarrito Get(int id_carrito)
        //{
        //    CarritosBD carritos = new CarritosBD();
        //    return carritos.get(id_carrito);
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<EntityCarrito> Create(EntityCarrito EntityCarrito)
        //{
        //   CarritosBD carritos = new CarritosBD();
            
        //    var result= carritos.GETALLCARRITOS(carritos.INSERTCARRITO(EntityCarrito));

        //    return CreatedAtAction(nameof(Get), new { id = EntityCarrito.ididentifier_i }, EntityCarrito);
        //}

        //[HttpPut]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<EntityCarrito> Update(EntityCarrito EntityCarrito)
        //{
        //    CarritosBD carritos = new CarritosBD();

        //    var result = carritos.GETCARRITO(carritos.INSERTCARRITO(EntityCarrito));

        //    return CreatedAtAction(nameof(Get), new { id = EntityCarrito.ididentifier_i }, EntityCarrito);
        //}

        //[HttpDelete]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<EntityCarrito> Delete(int id)
        //{
        //    CarritosBD carritos = new CarritosBD();

        //    EntityCarrito EntityCarrito = carritos.GETCARRITO(id);
        //    carritos.DELETECARRITO(EntityCarrito);
            
        //    return CreatedAtAction(nameof(Get), new { id = EntityCarrito.ididentifier_i }, EntityCarrito);
        //}

    }
}