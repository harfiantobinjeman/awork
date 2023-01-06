using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Dto.Sales
{
    public class GetDataPersonCustomer
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }
    }
}
