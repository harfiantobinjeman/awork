
using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SpecialOffer
{
    public class SpecialOfferForCreateDto
    {
        [Required(ErrorMessage = "Please Enter The Descriptions!")]
        public string Description { get; set; }
        public decimal DiscountPct { get; set; }

        [Required(ErrorMessage = "Please Enter The Type!")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Please Enter The Category!")]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int MinQty { get; set; }
        public int? MaxQty { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public virtual ICollection<SpecialOfferProductDto> SpecialOfferProductDto { get; set; }
        public virtual List<SpecialOfferProductForCreateDto> SpecialOfferProductForCreateDto { get; set; } = new List<SpecialOfferProductForCreateDto>();
    }
}
