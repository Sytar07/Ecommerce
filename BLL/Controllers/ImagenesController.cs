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
    public class ImagenesContoller : ControllerBase
    {
        [HttpGet(Name = "GetAllImagenes")]
        public IEnumerable<EntityImagen> GetAll()
        {
            ImagenesBD imagenes = new ImagenesBD();
            return imagenes.GETALLIMAGENES().lista;
        }


    }
}