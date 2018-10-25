using IntSol.Libraries.Business.Interfaces;
using IntSol.Libraries.Business.Validations.Interfaces;
using IntSol.Libraries.DataAccess.Interfaces;
using IntSol.Libraries.Models;
using System;
using System.Collections.Generic;

namespace IntSol.Libraries.Business.Impl
{
    public class CustomersBusinessService : ICustomersBusinessService
    {
        private const string INVALID_CUSTOMERS_REPOSITORY = @"Invalid Customers Repository Specified!";
        private ICustomersRepository customersRepository = default(ICustomersRepository);
        private ICustomerNameValidation customerNameValidation = default(ICustomerNameValidation);

        public CustomersBusinessService(ICustomersRepository customersRepository,
            ICustomerNameValidation customerNameValidation)
        {
            if (customersRepository == default(ICustomersRepository))
                throw new ArgumentException(INVALID_CUSTOMERS_REPOSITORY);

            this.customersRepository = customersRepository;
            this.customerNameValidation = customerNameValidation;
        }

        public bool AddNewCustomer(Customer newCustomer)
        {
            var saveStatus = default(bool);

            var validation = newCustomer != default(Customer) &&
                this.customersRepository != default(ICustomersRepository);

            if (validation)
                saveStatus = this.customersRepository.AddEntity(newCustomer);

            return saveStatus;
        }

        public void Dispose() => this.customersRepository?.Dispose();

        public Customer GetCustomer(int customerId)
        {
            var filteredCustomer = default(Customer);
            var validation = customerId != default(int) &&
                this.customersRepository != default(ICustomersRepository);

            if (validation)
                filteredCustomer = this.customersRepository.GetEntityByKey(customerId);

            return filteredCustomer;
        }

        public IEnumerable<Customer> GetCustomers(string customerName = null)
        {
            var customersList = default(IEnumerable<Customer>);

            if (string.IsNullOrEmpty(customerName))
                customersList = this.customersRepository.GetEntities();
            else
            {
                var validation = !string.IsNullOrEmpty(customerName) &&
                    !this.customerNameValidation.Validate(customerName);

                if (validation)
                    customersList = this.customersRepository.GetCustomersByName(customerName);
            }

            return customersList;
        }
    }
}
