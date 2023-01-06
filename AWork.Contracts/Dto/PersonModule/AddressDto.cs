using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AWork.Contracts.Dto.PersonModule
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }
        public int StateProvinceId { get; set; }

        [Required]
        [Display(Name ="Postal Code")]
        public string PostalCode { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }


        public virtual StateProvinceDto StateProvince { get; set; }
        public virtual ICollection<BusinessEntityAddressDto> BusinessEntityAddresses { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderHeaderBillToAddresses { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaderShipToAddresses { get; set; }
    }
}
