using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Domain.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "EMPLOYEE",
                    NormalizedName = "EM"
                },
                new IdentityRole
                {
                    Name = "SP",
                    NormalizedName = "SALES PERSON"
                },
                new IdentityRole
                {
                    Name = "IN",
                    NormalizedName = "INDIVIDUAL CUSTOMER"
                },
                new IdentityRole
                {
                    Name = "SC",
                    NormalizedName = "STORE CONTACT"
                },
                new IdentityRole
                {
                    Name = "VC",
                    NormalizedName = "VENDOR CONTACT"
                },
                new IdentityRole
                {
                    Name = "GC",
                    NormalizedName = "GENERAL CONTACT"
                }
                );
        }
    }
}
