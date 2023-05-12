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
    public class DireccionController : ControllerBase
    {

        private readonly ILogger<DireccionController> _logger;

        public DireccionController(ILogger<DireccionController> logger)
        {
            _logger = logger;
        }
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
            _logger.LogInformation($"API Create de direccion {entityDireccion.direccion_nv} las {DateTime.Now.ToLongTimeString()}");
            var result= Direcciones.GETDIRECCION(Direcciones.INSERTDIRECCION(entityDireccion));

            return CreatedAtAction(nameof(Get), new { id = result.ididentifier_i }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityDireccion> Update(EntityDireccion entityDireccion)
        {
            DireccionesBD Direcciones = new DireccionesBD();

            var result = Direcciones.GETDIRECCION(Direcciones.UPDATEDIRECCION(entityDireccion));

            return CreatedAtAction(nameof(Get), new { id = result.ididentifier_i }, result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityDireccion> Delete(int id_direccion)
        {
            DireccionesBD Direcciones = new DireccionesBD();

            EntityDireccion entityDireccion = Direcciones.GETDIRECCION(id_direccion);
            Direcciones.DELETEDIRECCION(entityDireccion);
            
            return CreatedAtAction(nameof(Get), new { id = entityDireccion.ididentifier_i }, entityDireccion);
        }

    }
}