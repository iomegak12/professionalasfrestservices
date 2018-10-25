using IntSol.Libraries.Models;
using IntSol.Libraries.ORM.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntSol.Libraries.ORM.Impl
{
    public class CustomersContext : BaseSystemContext, ICustomersContext
    {
        public CustomersContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Customer>(new CustomerEntityTypeConfiguration());
        }
    }
}
