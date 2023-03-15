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
    public class FormaPagoController : ControllerBase
    {
        

        /// <summary>
        /// API: GetUser
        /// Espera un ID y resuelve con la entidad del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetFormaPago")]
        public EntityFormasPago Get(int id_conexion)
        {
            FormasPagoBD formasPago = new FormasPagoBD();
            return formasPago.GETALLFORMASPAGO(id_formapago);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityFormasPago> Create(EntityFormasPago entityFormaPago)
        {
           FormasPagoBD formasPago = new FormasPagoBD();
            
            var result= formasPago.GETALLFORMASPAGO(formasPago.INSERTFORMASPAGO(entityFormaPago));

            return CreatedAtAction(nameof(Get), new { id = entityFormaPago.id_formapago }, entityFormaPago);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityFormasPago> Update(EntityFormasPago entityFormaPago)
        {
            FormasPagoBD formasPago = new FormasPagoBD();

            var result = formasPago.GETALLFORMASPAGO(formasPago.INSERTFORMASPAGO(entityFormaPago));

            return CreatedAtAction(nameof(Get), new { id = entityFormaPago.id_formapago }, entityFormaPago);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityFormasPago> Delete(int id)
        {
            FormasPagoBD formasPago = new FormasPagoBD();

            EntityFormasPago entityFormaPago = formasPago.GETALLFORMASPAGO(id);
            formasPago.DELETEFORMASPAGO(entityFormaPago);
            
            return CreatedAtAction(nameof(Get), new { id = entityFormaPago.id_formapago }, entityFormaPago);
        }

    }
}