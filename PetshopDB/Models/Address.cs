using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PetshopDB.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Column(TypeName = "varchar(9)")]
        public string Cep { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Street { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Number { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Complement { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string County { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string City { get; set; }

        [Column(TypeName = "varchar(2)")]
        public string State { get; set; }
    }
}
