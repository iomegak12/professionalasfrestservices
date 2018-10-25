using IntSol.Libraries.Models;
using System;
using System.Collections.Generic;

namespace IntSol.Libraries.Business.Interfaces
{
    public interface ICustomersBusinessService : IDisposable
    {
        IEnumerable<Customer> GetCustomers(string customerName = default(string));
        Customer GetCustomer(int customerId);
        bool AddNewCustomer(Customer newCustomer);
    }
}
