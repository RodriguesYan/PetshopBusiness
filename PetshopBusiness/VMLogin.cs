using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetshopBusiness
{
    public class VMLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public bool RememberMe { get; set; }
    }
}
