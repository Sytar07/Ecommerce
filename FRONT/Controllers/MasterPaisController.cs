using ECOMMERCE.CORE;
using FRONT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FRONT.Controllers
{
    public class MasterPaisController : Controller
    {
        private readonly ILogger<MasterPaisController> _logger;

        public MasterPaisController(ILogger<MasterPaisController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            EntityPaises Paises = new EntityPaises();
            Paises.list = new List<EntityPais>()
            {
                new EntityPais(1,"ESPAÑA"),
                new EntityPais(2,"FRANCIA"),
                new EntityPais(3,"SUECIA"),
                new EntityPais(4,"DINAMARCA"),
                new EntityPais(5,"MEXICO"),
                new EntityPais(6,"PORTUGAL"),
            };

            return View(Paises.list);
        }
        public IActionResult Details(int id)
        {

            return View(new EntityPais(id, "ESPAÑA"));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Vista y Modelo de ERROR. No tocar por ahora
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}