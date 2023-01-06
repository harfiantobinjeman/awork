using System;

namespace AWork.Contracts.Dto.Production
{
    public class LocationForCreateDto
    {
        public string Name { get; set; }
        public decimal CostRate { get; set; }
        public decimal Availability { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
