
using AWork.Contracts.Dto.Sales.SalesOrderDetail;
using AWork.Contracts.Dto.Sales.SpecialOfferProduct;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Sales.SpecialOffer
{
    public class SpecialOfferGroupDto
    {
        public SpecialOfferForCreateDto SpecialOfferForCreateDtos { get; set; }
        public SpecialOfferDto SpecialOfferDtos { get; set; }
        public SpecialOfferProductDto SpecialOfferProductDtos { get; set; }
        public List<SpecialOfferProductForCreateDto> SpecialOfferProductForCreateDtos { get; set; }
    }
}
