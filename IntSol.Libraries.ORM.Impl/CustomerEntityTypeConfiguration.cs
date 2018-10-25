using IntSol.Libraries.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntSol.Libraries.ORM.Impl
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            if (builder != default(EntityTypeBuilder<Customer>))
            {
                builder.Property(model => model.CustomerId)
                    .UseSqlServerIdentityColumn();

                builder.HasKey(model => model.CustomerId);

                builder.ToTable("Customers");
            }
        }
    }
}
