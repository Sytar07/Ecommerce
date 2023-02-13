using Microsoft.AspNetCore.Mvc;
using ECOMMERCE.CORE;
using ECOMMERCE.DAL;

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
            ECOMMERCE.DAL.UsersBD users = new ECOMMERCE.DAL.UsersBD();
            return users.GETALLUSERS().entityUsers.ToList();
        }
    }
}