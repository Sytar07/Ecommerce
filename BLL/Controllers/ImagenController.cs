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
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagenController : ControllerBase
    {

         
        /// <summary>
        /// API: GetUser
        /// Espera un ID y resuelve con la entidad del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetImagen")]
        public EntityImagen Get(int id_imagen)
        {
            ImagenesBD imagenes = new ImagenesBD();
            return imagenes.GETIMAGEN(id_imagen);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityImagen> Create(EntityImagen EntityImagen)
        {
           ImagenesBD imagenes = new ImagenesBD();
            
            var result= imagenes.GETIMAGEN(imagenes.INSERTIMAGEN(EntityImagen));

            return CreatedAtAction(nameof(Get), new { id = EntityImagen.ididentifier_i }, EntityImagen);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityImagen> Update(EntityImagen EntityImagen)
        {

            
            
            ImagenesBD imagenes = new ImagenesBD();

            var result = imagenes.GETIMAGEN(imagenes.UPDATEIMAGEN(EntityImagen));

            return CreatedAtAction(nameof(Get), new { id = EntityImagen.ididentifier_i }, EntityImagen);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityImagen> Delete(int id_Imagen)
        {
            ImagenesBD imagenes = new ImagenesBD();

            EntityImagen EntityImagen = imagenes.GETIMAGEN(id_Imagen);
            imagenes.DELETEIMAGEN(EntityImagen);
            
            return CreatedAtAction(nameof(Get), new { id = EntityImagen.ididentifier_i }, EntityImagen);
        }

    }
}


