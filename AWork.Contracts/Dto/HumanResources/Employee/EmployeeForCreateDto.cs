using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.HumanResources.Employee
{
    public class EmployeeForCreateDto
    {
        public int BusinessEntityId { get; set; }


        [Required(ErrorMessage = "National Id Number Required")]
        [StringLength(9)]
        public string NationalIdnumber { get; set; }


        public string LoginId { get; set; }
        public short? OrganizationLevel { get; set; }

        [Required(ErrorMessage = "Job Title Required")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Birth Date Required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Martial Status Required")]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "Gender Required")]
        public string Gender { get; set; }

        [Required(ErrorMessage="Hire Date Required")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        public bool? SalariedFlag { get; set; }

        [Required(ErrorMessage = "Vacation Hours Required")]
        [Range(0, 100,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public short VacationHours { get; set; }

        [Required(ErrorMessage = "Sick Leave Hours Required")]
        [Range(0, 100,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public short SickLeaveHours { get; set; }

        public bool? CurrentFlag { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
