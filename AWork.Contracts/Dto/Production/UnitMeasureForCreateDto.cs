using System;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto.Production
{
    public class UnitMeasureForCreateDto
    {
        [Required]
        [StringLength(3, ErrorMessage = "unit measure code tidak boleh lebih dari 3 karakter")]
        public string UnitMeasureCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
