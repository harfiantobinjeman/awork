using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Dto.Person
{
    public class LookUpCountryRegionDanStateprovince
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public string StateProvinceCode { get; set; }
        public string Province { get; set; }
        public int StateProvinceId { get; set; }
    }
}
