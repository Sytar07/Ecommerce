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
    public class ConexionController : ControllerBase
    {
        

        /// <summary>
        /// API: GetUser
        /// Espera un ID y resuelve con la entidad del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public EntityConexion Get(int id_conexion)
        {
            ConexionesBD conexiones = new ConexionesBD();
            return conexiones.GETCONEXION(id_conexion);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityConexion> Create(EntityConexion entityConexion)
        {
           ConexionesBD conexiones = new ConexionesBD();
            
            var result= conexiones.GETCONEXION(conexiones.INSERTCONEXION(entityConexion));

            return CreatedAtAction(nameof(Get), new { id = entityConexion.ididentifier_i }, entityConexion);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityConexion> Update(EntityConexion entityConexion)
        {
            ConexionesBD conexiones = new ConexionesBD();

            var result = conexiones.GETCONEXION(conexiones.INSERTCONEXION(entityConexion));

            return CreatedAtAction(nameof(Get), new { id = entityConexion.ididentifier_i }, entityConexion);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityConexion> Delete(int id)
        {
            ConexionesBD conexiones = new ConexionesBD();

            EntityConexion entityConexion = conexiones.GETCONEXION(id);
            conexiones.DELETECONEXION(entityConexion);
            
            return CreatedAtAction(nameof(Get), new { id = entityConexion.ididentifier_i }, entityConexion);
        }

    }
}