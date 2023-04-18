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
    public class FormaPagoController : ControllerBase
    {

        private readonly ILogger<FormaPagoController> _logger;

        public FormaPagoController(ILogger<FormaPagoController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// API: GetUser
        /// Espera un ID y resuelve con la entidad del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetFormaPago")]
        public EntityFormaPago Get(int id_fpa)
        {
            FormasPagoBD formasPago = new FormasPagoBD();
            return formasPago.GETFORMAPAGO(id_fpa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityFormaPago> Create(EntityFormaPago entityFormaPago)
        {
           FormasPagoBD formasPago = new FormasPagoBD();
            _logger.LogInformation($"API Create de forma pago {entityFormaPago.name_nv} las {DateTime.Now.ToLongTimeString()}");
            var result= formasPago.GETFORMAPAGO(formasPago.INSERTFORMAPAGO(entityFormaPago));

            return CreatedAtAction(nameof(Get), new { id = entityFormaPago.ididentifier_i }, entityFormaPago);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityFormaPago> Update(EntityFormaPago entityFormaPago)
        {
            FormasPagoBD formasPago = new FormasPagoBD();

            var result = formasPago.GETFORMAPAGO(formasPago.UPDATEFORMAPAGO(entityFormaPago));

            return CreatedAtAction(nameof(Get), new { id = entityFormaPago.ididentifier_i }, entityFormaPago);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityFormasPago> Delete(int id_fpa)
        {
            FormasPagoBD formasPago = new FormasPagoBD();

            EntityFormaPago entityFormaPago = formasPago.GETFORMAPAGO(id_fpa);
            formasPago.DELETEFORMAPAGO(entityFormaPago);
            
            return CreatedAtAction(nameof(Get), new { id = entityFormaPago.ididentifier_i }, entityFormaPago);
        }

    }
}