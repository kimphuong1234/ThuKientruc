using Dapper;
using MISA.ApplicationCore.Entities;
using MISA.Entity.Model;
using MISA.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MISA.ApplicationCore
{
    public class CustomerService
    {
        #region Method
        //Lấy danh sách khách hàng
        public IEnumerable<Customer> GetCustomers()
        {
            var customerContext = new CustomerContext();
            var customers = customerContext.GetCustomers();
            return customers;
        }
        //Thêm mới khách hàng
        public ServiceResult InsertCustomer(Customer customer)
        {
            var serviceResult = new ServiceResult();
            var customerContext = new CustomerContext();
            //validate dữ liệu
            //Check trường bắt buộc nhập, nếu dữ liệu chưa hợp lệ thì mô tả lỗi
            var customerCode = customer.CustomerCode;
            if (string.IsNullOrEmpty(customerCode))
            {
                var msg = new
                {
                    devMsg = new { fieldName = "CustomerCode", msg = "Ma khach hang khong duoc phep de trong" },
                    userMsg = "Mã khách hàng không được phép để trống",
                    Code = 900, // Mã validate sai

                };
                serviceResult.MISACode = 900;
                serviceResult.Messenger = "Mã khách hàng không được phép để trống";
                serviceResult.Data = msg;
                return serviceResult;
            }
                
            //check trùng mã:
            var res = customerContext.GetCustomerByCode(customerCode);
            if (res != null)
            {
                var msg = new
                {
                    devMsg = new { fieldName = "CustomerCode", msg = "Ma khach hang da ton tai" },
                    userMsg = "Mã khách hàng đã tồn tại",
                    Code = 900, // Mã validate sai

                };
                serviceResult.MISACode = 900;
                serviceResult.Messenger = "Mã khách hàng không được phép để trống";
                serviceResult.Data = msg;
                return serviceResult; 
            }
            
            //Thêm mới dữ liệu đã hợp lệ

            var rowAffects = customerContext.InsertCustomer(customer);
            serviceResult.MISACode = 100;
            serviceResult.Messenger = "Thêm thành công";
            serviceResult.Data = rowAffects;
            return serviceResult;
        }
        
        //Sửa thông tin khách hàng

        //Xóa khách hàng theo khóa chính
        #endregion
    }
}
