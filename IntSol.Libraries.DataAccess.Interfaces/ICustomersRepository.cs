using IntSol.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntSol.Libraries.DataAccess.Interfaces
{
    public interface ICustomersRepository : IRepository<Customer, int>
    {
        IEnumerable<Customer> GetCustomersByName(string customerName);
    }
}
