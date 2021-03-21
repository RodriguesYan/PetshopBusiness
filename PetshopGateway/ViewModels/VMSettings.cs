using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetshopGateway.ViewModels
{
    public class VMSettings
    {
        public VMAddress Address { get; set; }
    }

    public class VMAddress
    {
        public int AddressId { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ClientUserId { get; set; }
    }
}
