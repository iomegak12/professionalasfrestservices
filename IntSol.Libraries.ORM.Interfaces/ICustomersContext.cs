using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using IntSol.Libraries.Models;

namespace IntSol.Libraries.ORM.Interfaces
{
    public interface ICustomersContext : ISystemContext
    {
        DbSet<Customer> Customers { get; set; }
    }
}
