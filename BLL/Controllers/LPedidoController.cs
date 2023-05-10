using Microsoft.AspNetCore.Mvc;
using ECOMMERCE.CORE;
using ECOMMERCE.DAL;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ECOMMERCE.DAL;
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
    public class LPedidoController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<EntityLineaPedido> GetAll(int id)
        {
            PedidosBD Pedido = new PedidosBD();
            return Pedido.GET_LPedido(id);
        }


    }
}