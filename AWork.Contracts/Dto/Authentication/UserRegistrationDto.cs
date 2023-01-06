using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Authentication
{
    public class UserRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        public string PersonType { get; set; }
        public string Suffix { get; set; }
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int BusinessEntityId { get; set; }

        public int BusinessEntity { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Not match Password and Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remeber Me")]
        public bool RememberMe { get; set; }


    }
}
