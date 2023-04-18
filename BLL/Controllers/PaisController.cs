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
    public class PaisController : ControllerBase
    {

        private readonly ILogger<PaisController> _logger;

        public PaisController(ILogger<PaisController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// API: GetUser
        /// Espera un ID y resuelve con la entidad del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public EntityPais Get(int id_pais)
        {
            PaisesBD paises = new PaisesBD();
            return paises.GETPAIS(id_pais);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityPais> Create(EntityPais entityPais)
        {
           PaisesBD paises = new PaisesBD();
            
            var result= paises.GETPAIS(paises.INSERTPAIS(entityPais));

            return CreatedAtAction(nameof(Get), new { id = entityPais.ididentifier_i }, entityPais);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityPais> Update(EntityPais entityPais)
        {
            PaisesBD paises = new PaisesBD();

            var result = paises.GETPAIS(paises.INSERTPAIS(entityPais));
            _logger.LogInformation($"API Create de pais {entityPais.name_nv} las {DateTime.Now.ToLongTimeString()}");
            return CreatedAtAction(nameof(Get), new { id = entityPais.ididentifier_i }, entityPais);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityPais> Delete(int id)
        {
            PaisesBD paises = new PaisesBD();

            EntityPais entityPais = paises.GETPAIS(id);
            paises.DELETEPAIS(entityPais);
            
            return CreatedAtAction(nameof(Get), new { id = entityPais.ididentifier_i }, entityPais);
        }

    }
}