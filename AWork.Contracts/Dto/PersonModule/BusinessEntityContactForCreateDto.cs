using System;

namespace AWork.Contracts.Dto
{
    public class BusinessEntityContactForCreateDto
    {
        public int BusinessEntityId { get; set; }
        public int PersonId { get; set; }
        public int ContactTypeId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }


    }
}
