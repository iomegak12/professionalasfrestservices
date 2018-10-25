using IntSol.Libraries.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IntSol.Libraries.Api.Controllers.Interfaces
{
    public interface ICustomersApiController
    {
        IActionResult GetCustomers();
        IActionResult SearchCustomers(string customerName);
        IActionResult GetCustomerDetail(int customerId);
        IActionResult SaveCustomerDetail(Customer customer);
    }
}
