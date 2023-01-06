using AWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AWork.Domain.Dto.Purchasing
{
    public class GetShipMethod
    {
        public int ShipMethodId { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }
    }
}
