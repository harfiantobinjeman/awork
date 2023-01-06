using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SpecialOffer
{
    public class SpecialOfferDto
    {
        public int SpecialOfferId { get; set; }
        public string Description { get; set; }
        public decimal DiscountPct { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinQty { get; set; }
        public int? MaxQty { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<SpecialOfferProductDto> SpecialOfferProductDto { get; set; }
        public virtual SpecialOfferGroupDto SpecialOfferGroupDto { get; set; }

        public virtual List<SpecialOfferProductDto> SpecialOfferProductDtoss { get; set; }
        public virtual List<SpecialOfferProductForCreateDto> SpecialOfferProductForCreateDto { get; set; } = new List<SpecialOfferProductForCreateDto>();
    }
}
