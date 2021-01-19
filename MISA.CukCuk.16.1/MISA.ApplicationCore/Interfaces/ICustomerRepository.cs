using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface
{
    /// <summary>
    /// Interface danh muc khach hang 
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Lay danh sach khach hang
        /// </summary>
        /// <returns>Danh sasch khach hang</returns>
        /// CreatedBy: ABC (19/01/2021)
        IEnumerable<Customer> GetCustomers();
        /// <summary>
        /// Dua ra khach hang theo ma
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns>Khach hang</returns>
        /// CreatedBy: ABC(19/01/2021)
        Customer GetCustomerByCode(string customerCode);
        /// <summary>
        /// Them khach hang
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Khach hang duoc them</returns>
        /// CreatedBy: Abc(19/01/2021)
        int AddCustomer(Customer customer);
        /// <summary>
        /// Them khach hang
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Khach hang duoc them</returns>
        /// CreatedBy: Abc(19/01/2021)
        int UpdateCustomer(Customer customer);
        int DeleteCustomer(string customerCode);
      
    }
}
