using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CotizadorKober.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Usuario ")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Contraseña ")]
        public string Password { get; set; }
    }
}
