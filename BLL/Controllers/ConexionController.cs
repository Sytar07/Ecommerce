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
        
        [HttpGet]
        public EntityConexion Get(string IP,string User )
        {
            ConexionesBD conexiones = new ConexionesBD();
            return conexiones.GETCONEXION(IP,   User);
        }

       

    }
}