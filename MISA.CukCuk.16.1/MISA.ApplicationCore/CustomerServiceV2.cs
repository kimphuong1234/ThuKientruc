using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore
{
    public class CustomerServiceV2 : ICustomerService
    {
        public ServiceResult AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteCustomer(string customerCode)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByCode(string customerCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public ServiceResult UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
