using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Authentication
{
    public class UserLoginDto
    {


        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Remeber Me")]
        public bool RememberMe { get; set; }

    }
}
