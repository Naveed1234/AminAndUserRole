using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AminAndUserRole.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name required")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "C_Password required")]
        [DataType(DataType.Password)Compare("Password",ErrorMessage = "Pasword not Match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Address required")]
        [DataType(DataType.Text)]
        public string Address { get; set; }
        [Required(ErrorMessage = "PostalCode required")]
        [DataType(DataType.Text)]
        public int PostalCode { get; set; }
        [Required(ErrorMessage = "Email required")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        public virtual Role Role { get; set; }

        public bool IsinRole(int id)
        {
            return (Role != null && Role.Id == id);
        }
    }
}