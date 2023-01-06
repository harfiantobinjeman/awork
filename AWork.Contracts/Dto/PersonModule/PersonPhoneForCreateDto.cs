using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto
{
    public class PersonPhoneForCreateDto
    {
        public int BusinessEntityId { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public DateTime ModifiedDate { get; set; }
        

    }
}
