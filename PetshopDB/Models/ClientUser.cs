using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetshopDB.Models
{
    public class ClientUser
    {
        [Key]
        public int ClientUserId { get; set; }
        [Column(TypeName = "varchar(60)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Login { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }
        [Column(TypeName = "int")]
        public int ConfirmationCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime InsertDate { get; set; }
        [Column(TypeName = "datetime")]
        public Nullable<DateTime> UpdateDate { get; set; }
        [Column(TypeName = "datetime")]
        public Nullable<DateTime> DeleteDate { get; set; }

        [NotMapped]
        public string Role { get; set; }
        [NotMapped]
        public string FormValue { get; set; }
    }

    //public class ClientUser
    //{
    //    public int Id { get; set; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public string Role { get; set; }
    //}
}
