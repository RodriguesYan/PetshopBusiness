using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetshopGateway.Petshop;
using PetshopGateway.ViewModels;

namespace PetshopBusiness.Controllers
{
    public class MyAccountController : Controller
    {
        Api _api;
        string jwtToken = "";

        public MyAccountController()
        {
            _api = new Api();
            //jwtToken = GetJwtToken("Token");
        }

        public async Task<IActionResult> Settings()
        {
            SetToken();
            _api = new Api(jwtToken);
            VMSettings vm = new VMSettings();
            vm.Address = await _api.GetAddress(GetUserId());

            if (HttpContext.User.Claims.Any(t => t.Type == "Token"))
            {
                jwtToken = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token").Value;
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(VMAddress vm)
        {
            SetToken();
            _api = new Api(jwtToken);
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
            vm.ClientUserId = Convert.ToInt32(id);
            var result = "";

            if (vm.AddressId > 0)
            {
                result = await _api.EditAddress(vm, jwtToken);
            }
            else
            {
                result = await _api.CreateAddress(vm, jwtToken);
            }


            return RedirectToAction("Settings");
        }

        private void SetToken()
        {
            if (HttpContext.User.Claims.Any(t => t.Type == "Token"))
            {
                jwtToken = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token").Value;
            }
        }

        private int GetUserId()
        {
            return Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        }
    }
}