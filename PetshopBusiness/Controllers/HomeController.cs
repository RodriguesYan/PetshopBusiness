using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PetshopBusiness.Models;
using PetshopGateway.Petshop;

namespace PetshopBusiness.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IApi _api;
        Api _api;
        //private readonly HttpClient _httpCLient;
        //protected BeMealsGateway.BeMeals.Api api = new BeMealsGateway.BeMeals.Api();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _api = new Api();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccount(VMLogin login, string formValue)
        {
            PetshopGateway.Petshop.LoginResult result = new PetshopGateway.Petshop.LoginResult();
            if (formValue == "register")
            {
                result = await _api.CreateUser(login);

                if (result.Succeeded)
                {

                }
            }
            else
            {
                result = await _api.ExecuteLogin(login);

                if (result.Succeeded)
                {

                }
            }

            if (result.Succeeded)
            {
                //_api.Token = result.Token;

                var userObject = JsonConvert.SerializeObject(result.LoginProperties.User);

                HttpContext.Session.SetString("Token", result.LoginProperties.Token);
                //HttpContext.Session.SetString("Token", result.LoginProperties.Token);
                HttpContext.Session.SetString("User", userObject);
                //Pra pegar => HttpContext.Session.GetString("Token")
                //var token = HttpContext.Session.GetString("Token");
                //Para armazena obj =>
                //var str = JsonConvert.SerializeObject(obj);
                //HttpContext.Session.SetString("OBJECT", str);


            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(VMLogin login, string formValue)
        {
            PetshopGateway.Petshop.LoginResult result = new PetshopGateway.Petshop.LoginResult();

            result = await _api.ExecuteLogin(login);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("loginError", result.ErrorMessage);

                return View(login);
            }

            //_api.Token = result.Token;

            var userObject = JsonConvert.SerializeObject(result.LoginProperties.User);

            HttpContext.Session.SetString("Token", result.LoginProperties.Token);
            //HttpContext.Session.SetString("Token", result.LoginProperties.Token);
            HttpContext.Session.SetString("User", userObject);
            //Pra pegar => HttpContext.Session.GetString("Token")
            //var token = HttpContext.Session.GetString("Token");
            //Para armazena obj =>
            //var str = JsonConvert.SerializeObject(obj);
            //HttpContext.Session.SetString("OBJECT", str);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class LoginResult
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
    }
}
