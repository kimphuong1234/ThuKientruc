using MISA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Infrastructure
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerByCode(string customerCode);
        int AddCustomer(Customer customer);
        int UpdateCustomer(Customer customer);
        int DeleteCustomer(string customerCode);
    }
}
