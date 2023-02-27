using Microsoft.AspNetCore.Mvc;
using ECOMMERCE.CORE;
using ECOMMERCE.DAL;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ECOMMERCE.DAL;
using DAL;

namespace BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {       

        private readonly ILogger<UsersController> _logger;


        [HttpGet(Name = "GetAllUsers")]
        public IEnumerable<EntityUser> Get()
        {
            UsersBD users = new UsersBD();
            return users.GETALLUSERS().lista;
        }
    }
}