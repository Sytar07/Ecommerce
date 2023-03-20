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
    public class DireccionController : ControllerBase
    {
        

        /// <summary>
        /// API: GetUser
        /// Espera un ID y resuelve con la entidad del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetDireccion")]
        public EntityDireccion Get(int id_Direccion)
        {
            DireccionesBD Direcciones = new DireccionesBD();
            return Direcciones.GETDIRECCION(id_Direccion);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityDireccion> Create(EntityDireccion entityDireccion)
        {
           DireccionesBD Direcciones = new DireccionesBD();
            
            var result= Direcciones.GETDIRECCION(Direcciones.INSERTDIRECCION(entityDireccion));

            return CreatedAtAction(nameof(Get), new { id = entityDireccion.ididentifier_i }, entityDireccion);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityDireccion> Update(EntityDireccion entityDireccion)
        {
            DireccionesBD Direcciones = new DireccionesBD();

            var result = Direcciones.GETDIRECCION(Direcciones.INSERTDIRECCION(entityDireccion));

            return CreatedAtAction(nameof(Get), new { id = entityDireccion.ididentifier_i }, entityDireccion);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityDireccion> Delete(int id)
        {
            DireccionesBD Direcciones = new DireccionesBD();

            EntityDireccion entityDireccion = Direcciones.GETDIRECCION(id);
            Direcciones.DELETEDIRECCION(entityDireccion);
            
            return CreatedAtAction(nameof(Get), new { id = entityDireccion.ididentifier_i }, entityDireccion);
        }

    }
}