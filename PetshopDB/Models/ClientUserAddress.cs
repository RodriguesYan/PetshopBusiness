using System;
using System.Collections.Generic;
using System.Text;

namespace PetshopDB.Models
{
    public class ClientUserAddress
    {
        public int ClientUserAddressId { get; set; }
        public int ClientUserId { get; set; }
        public int AddressId { get; set; }
        public bool IsDefault { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public Nullable<DateTime> DeleteDate { get; set; }
    }
}
