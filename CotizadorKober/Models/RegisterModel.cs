using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CotizadorKober.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Usuario ")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "E-Mail ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Password ")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Confirm Password ")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre ")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "Apellidos ")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "Lista ")]
        public string Lista { get; set; }
    }
}
