using ECOMMERCE.CORE;
using FRONT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
            Paises.lista = new List<EntityPais>()
            {
                new EntityPais()
                {
                    ididentifier_i=1,
                    name_nv="ESP"
                },
            };

            return View(Paises.lista);
        }
        public IActionResult Details(int id)
        {

            return View(new EntityPais()
            {
                ididentifier_i = 1,
                name_nv = "ESP"
            }
            );
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