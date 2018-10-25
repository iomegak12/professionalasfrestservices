using IntSol.Libraries.Api.Controllers.Interfaces;
using IntSol.Libraries.Business.Interfaces;
using IntSol.Libraries.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IntSol.Libraries.Api.Controllers.Impl
{
    /// <summary>
    /// Customers API Controller for Customers Information System
    /// </summary>
    [Route("api/customers")]
    [Produces(contentType: "application/json")]
    public class CustomersApiController : Controller, ICustomersApiController
    {
        private const string INVALID_BUSINESS_COMPONENT = "Invalid Customers Business Component Specified!";
        private ICustomersBusinessService customersBusinessService = default(ICustomersBusinessService);

        /// <summary>
        /// Default Constructor API Controller
        /// </summary>
        /// <param name="customersBusinessService">Valid Customers Business Component</param>
        public CustomersApiController(ICustomersBusinessService customersBusinessService)
        {
            if (customersBusinessService == default(ICustomersBusinessService))
                throw new ArgumentException(INVALID_BUSINESS_COMPONENT);

            this.customersBusinessService = customersBusinessService;
        }

        /// <summary>
        /// Gets Customer Details by Id
        /// </summary>
        /// <param name="customerId">Valid Customer Id</param>
        /// <returns>Customer Detail Object</returns>
        [Route("detail/{customerId}")]
        public IActionResult GetCustomerDetail(int customerId)
        {
            var validation = customerId != default(int);

            if (!validation)
                return BadRequest();

            var filteredCustomer = this.customersBusinessService.GetCustomer(customerId);

            if (filteredCustomer == default(Customer))
                return NotFound();

            return Ok(filteredCustomer);
        }

        /// <summary>
        /// Gets all Customer Records
        /// </summary>
        /// <returns>Array of Customer Records</returns>
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customersList =
                this.customersBusinessService.GetCustomers();

            return Ok(customersList);
        }

        /// <summary>
        /// Saves a New Customer Record
        /// </summary>
        /// <param name="customer">Valid Customer Record</param>
        /// <returns>Boolean</returns>
        [HttpPost]
        public IActionResult SaveCustomerDetail(
            [FromBody]
            Customer customer)
        {
            var validation = customer != default(Customer);

            if (!validation)
                return BadRequest();

            var saveStatus = this.customersBusinessService.AddNewCustomer(customer);

            return Ok(saveStatus);
        }

        /// <summary>
        /// Searches customer records by valid Customer Name
        /// </summary>
        /// <param name="customerName">Valid Search String</param>
        /// <returns>Array of Customer Recors</returns>

        [HttpGet]
        [Route("search/{customerName}")]
        public IActionResult SearchCustomers(string customerName)
        {
            var validation = !string.IsNullOrEmpty(customerName);

            if (!validation)
                return BadRequest();

            var filteredCustomers =
                this.customersBusinessService.GetCustomers(customerName);

            return Ok(filteredCustomers);
        }
    }
}
