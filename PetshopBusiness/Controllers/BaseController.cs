using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetshopBusiness.Controllers
{
    public class BaseController : Controller
    {
        //private readonly IHttpContextAccessor accessor;
        //public string GetJwtToken(string key)
        //{
        //    return accessor.HttpContext.Session.GetString(key);
        //}

        //public void SetStringToSession(string key, string value)
        //{
        //    accessor.HttpContext.Session.SetString(key, value);
        //}
    }
}