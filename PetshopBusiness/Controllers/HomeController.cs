using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            var token = "";
            if(HttpContext.User.Claims.Any(t => t.Type == "Token"))
            {
                token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token").Value;
            }

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

        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccount(VMLogin login, string formValue)
        {
            PetshopGateway.Petshop.LoginResult result = new PetshopGateway.Petshop.LoginResult();
            result = await _api.CreateUser(login);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("signUpError", result.ErrorMessage);
                login.Email = "";
                login.Password = "";
                return View(login);
            }

            var userObject = JsonConvert.SerializeObject(result.LoginProperties.User);

            //SetStringToSession("Token", result.LoginProperties.Token);
            //SetStringToSession("User", userObject);

            HttpContext.Session.SetString("Token", result.LoginProperties.Token);
            HttpContext.Session.SetString("User", userObject);

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> Login()
        //{
        //    await HttpContext.SignOutAsync();
        //    return View();
        //}

        [HttpPost]
        public async Task<ActionResult> Login(VMLogin login, string formValue)
        {
            PetshopGateway.Petshop.LoginResult result = new PetshopGateway.Petshop.LoginResult();

            result = await _api.ExecuteLogin(login);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("loginError", result.ErrorMessage);
                login.Email = "";
                login.Password = "";
                return View(login);
            }

            //_api.Token = result.Token;

            var userObject = JsonConvert.SerializeObject(result.LoginProperties.User);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.LoginProperties.User.Login),
                new Claim("Token", result.LoginProperties.Token),
                new Claim("UserId", result.LoginProperties.User.ClientUserId.ToString()),
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            HttpContext.Session.SetString("Token", result.LoginProperties.Token);
            HttpContext.Session.SetString("User", userObject);
            HttpContext.Session.SetString("UserId", result.LoginProperties.User.ClientUserId.ToString());

            //SetStringToSession("Token", result.LoginProperties.Token);
            //SetStringToSession("User", userObject);
            //SetStringToSession("UserId", result.LoginProperties.User.ClientUserId.ToString());


            //Pra pegar => HttpContext.Session.GetString("Token")
            //var token = HttpContext.Session.GetString("Token");
            //Para armazena obj =>
            //var str = JsonConvert.SerializeObject(obj);
            //HttpContext.Session.SetString("OBJECT", str);

            return RedirectToAction("Index");
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
