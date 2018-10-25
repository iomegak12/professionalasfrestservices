using IntSol.Libraries.DataAccess.Interfaces;
using IntSol.Libraries.Models;
using IntSol.Libraries.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntSol.Libraries.DataAccess.Impl
{
    public class CustomersRepository : ICustomersRepository
    {
        private const string INVALID_CUSTOMERS_CONTEXT = @"Invalid Customers Context Specified!";
        private ICustomersContext customersContext = default(ICustomersContext);

        public CustomersRepository(ICustomersContext customersContext)
        {
            if (customersContext == default(ICustomersContext))
                throw new ArgumentException(INVALID_CUSTOMERS_CONTEXT);

            this.customersContext = customersContext;
        }

        public bool AddEntity(Customer entity)
        {
            var status = default(bool);
            var validation = entity != default(Customer) &&
                this.customersContext != default(ICustomersContext);

            if (validation)
            {
                this.customersContext.Customers.Add(entity);
                status = this.customersContext.CommitChanges();
            }

            return status;
        }

        public void Dispose() => this.customersContext?.Dispose();

        public IEnumerable<Customer> GetCustomersByName(string customerName)
        {
            var filteredCustomers = default(IEnumerable<Customer>);
            var validation = !string.IsNullOrEmpty(customerName) &&
                customersContext != default(ICustomersContext);

            if (validation)
                filteredCustomers = this.customersContext.Customers.Where(
                    customer => customer.CustomerName.Contains(customerName)).ToList();

            return filteredCustomers;
        }

        public IEnumerable<Customer> GetEntities()
        {
            var customersList = default(IEnumerable<Customer>);

            if (this.customersContext != default(ICustomersContext))
                customersList = this.customersContext.Customers.ToList();

            return customersList;
        }

        public Customer GetEntityByKey(int entityKey)
        {
            var validation = entityKey != default(int) &&
                this.customersContext != default(ICustomersContext);
            var filteredCustomer = default(Customer);

            if (validation)
                filteredCustomer = this.customersContext.Customers.Where(
                    customer => customer.CustomerId.Equals(entityKey)).FirstOrDefault();

            return filteredCustomer;
        }
    }
}
