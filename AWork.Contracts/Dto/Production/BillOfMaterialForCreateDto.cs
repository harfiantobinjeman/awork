using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Production
{
    public class BillOfMaterialForCreateDto
    {
       
        public int BillOfMaterialsId { get; set; }
        public int? ProductAssemblyId { get; set; }
        public int ComponentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string UnitMeasureCode { get; set; }
        public short Bomlevel { get; set; }
        public decimal PerAssemblyQty { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ProductDto Product { get; set; }

        public virtual UnitMeasureDto UnitMeasure { get; set; }
       /* public virtual Product ProductAssembly { get; set; }
        public virtual UnitMeasureDto UnitMeasureCodeNavigation { get; set; }*/

    }
}
