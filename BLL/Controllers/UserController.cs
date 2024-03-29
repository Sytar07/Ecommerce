using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ECOMMERCE.CORE;

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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// API: GetUser
        /// Espera un ID y resuelve con la entidad del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetUser")]
        public EntityUser Get(int id_user)
        {
            UsersBD users = new UsersBD();
            return users.GETUSER(id_user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityUser> Create(EntityUser entityUser)
        {
            UsersBD users = new UsersBD();
            _logger.LogInformation($"API Create de usuario {entityUser.name_nv} las {DateTime.Now.ToLongTimeString()}");
            var result= users.GETUSER(users.INSERTUSER(entityUser));

            return CreatedAtAction(nameof(Get), new { id = entityUser.ididentifier_i }, entityUser);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityUser> Update(EntityUser entityUser)
        {
            UsersBD users = new UsersBD();
            _logger.LogInformation($"API Grabación de {entityUser.ididentifier_i.ToString()} a las {DateTime.Now.ToLongTimeString()}");
            var result = users.GETUSER(users.UPDATEUSER(entityUser));

            return CreatedAtAction(nameof(Get), new { id = entityUser.ididentifier_i }, entityUser);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EntityUser> Delete(int id_user)
        {
            UsersBD users = new UsersBD();
            EntityUser entityUser = users.GETUSER(id_user);
            users.DELETEUSER(entityUser);

            return CreatedAtAction(nameof(Get), new { id = entityUser.ididentifier_i }, entityUser);
        }

    }
}