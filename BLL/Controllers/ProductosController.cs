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
    public class ProductosController : ControllerBase
    {
        [HttpGet(Name = "GetAllControllers")]
        public IEnumerable<EntityProducto> GetAll()
        {
            ProductosBD productos = new ProductosBD();
            return productos.GETALLPRODUCTOS().lista;

        }


    }
}