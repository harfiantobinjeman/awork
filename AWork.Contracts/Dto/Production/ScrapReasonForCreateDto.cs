using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Production
{
    public class ScrapReasonForCreateDto
    {
        public short ScrapReasonId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
