using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.HumanResources.EmployeePayHistory
{
    public class EmployeePHForCreateDto
    {
        public int BusinessEntityId { get; set; }
        public DateTime RateChangeDate { get; set; }

        [Required(ErrorMessage ="Rate Required")]
        [Range(0, 200,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "Rate Required")]
        [Range(1, 2,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public byte PayFrequency { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
