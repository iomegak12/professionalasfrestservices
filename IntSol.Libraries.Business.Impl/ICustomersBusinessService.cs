using System;
using System.Collections.Generic;
using Yokogawa.Libraries.Models;

namespace Yokogawa.Libraries.Business.Interfaces
{
    public interface ICustomersBusinessService : IDisposable
    {
        IEnumerable<Customer> GetCustomers(string customerName = default(string));
        Customer GetCustomer(int customerId);
        bool AddNewCustomer(Customer newCustomer);
    }
}
