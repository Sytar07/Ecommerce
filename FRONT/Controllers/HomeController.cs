﻿using ECOMMERCE.CORE;
using FRONT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FRONT.Controllers
{
    public class HomeController : Controller
    {
        private const string apiUrlactions = "https://localhost:7023/User?id_user={0}";
        private const string apiUrlConexion = "https://localhost:7023/Conexion?IP={0}&User={1}&conexion=0";


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {            
            _logger = logger;            
        }

        public IActionResult Index()
        {
           

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(EntityUser model)
        {
            
            ModelState.Remove("clave_nvConfirm");
            if (ModelState.IsValid)
            {
                _logger.LogWarning($"Login de {model.email_nv} a las {DateTime.Now.ToLongTimeString()} ");
                DAL.UsersBD usersBD = new DAL.UsersBD();
                List<EntityUser> Users = usersBD.GETALLUSERS();

                List<EntityUser> User = Users.Where(s => s.email_nv.ToUpper().Contains(model.email_nv.ToUpper())).ToList();


                if (User.Count>0)
                {

                    if (User.First().clave_nv == model.clave_nv)
                    {
                        HttpContext.Session.SetString("UserName", model.email_nv);
                        HttpContext.Session.SetString("UserId", User.First().ididentifier_i.ToString());

                        if (User.First().rol_i == 9)
                        {
                            HttpContext.Session.SetString("Admin", "ADMIN");
                        }
                        // si estamos con un usuario debemos ignorar su IP. por eso le paso el nombre de usuario en la IP
                        EntityConexion conexion= GetConexion(model.email_nv.ToUpper(), model.email_nv.ToUpper());
                        if (conexion != null)
                        {
                            HttpContext.Session.SetString("Conexion", conexion.ididentifier_i.ToString());
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private static EntityConexion GetConexion(string ip, string email)
        {

            EntityConexion conexion = new EntityConexion(); 

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(string.Format(apiUrlConexion,ip,email)).Result;
            if (response.IsSuccessStatusCode)
            {
                conexion = JsonSerializer.Deserialize<EntityConexion>(response.Content.ReadAsStringAsync().Result);
            }

            return conexion;
        }

        [HttpGet]
        public IActionResult Register()
        {
            EntityUser entityUser = new EntityUser();
            return View(entityUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Register(EntityUser entityUser)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Grabación de nuevo  {entityUser.email_nv} a las {DateTime.Now.ToLongTimeString()}");
                // Si es valido grabamos y salimos al Index.
                CreateUser(entityUser).Wait();
                return RedirectToAction("Login");
            }
            // en los demás casos mostramos en pantalla
            return View("Edit", entityUser);
        }

        private static async Task<EntityUser> CreateUser(EntityUser entityUser)
        {
            string apiUrl = string.Format(apiUrlactions, entityUser.ididentifier_i);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, entityUser);
            response.EnsureSuccessStatusCode();

            entityUser = await response.Content.ReadFromJsonAsync<EntityUser>();

            return entityUser;
        }

    }
}