using System;

namespace AWork.Contracts.Dto
{
    public class PasswordDto
    {
        public int BusinessEntityId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
