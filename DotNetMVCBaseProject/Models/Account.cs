using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetMVCBaseProject.Models
{
    public class Account
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
        public bool Gender { get; set; }
        public Common.Enums.AccountRole Role { get; set; }
    }
}