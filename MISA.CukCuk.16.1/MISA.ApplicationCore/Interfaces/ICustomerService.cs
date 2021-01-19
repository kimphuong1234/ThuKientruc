using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerByCode(string customerCode);
        ServiceResult AddCustomer(Customer customer);
        ServiceResult UpdateCustomer(Customer customer);
        ServiceResult DeleteCustomer(string customerCode);
    }
}
