using Dapper;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MISA.ApplicationCore
{
    public class CustomerService:ICustomerService

    {
        ICustomerRepository _customerRepository;
        #region Constructor
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion
        #region Method
        //Lấy danh sách khách hàng
        public IEnumerable<Customer> GetCustomers()
        {
            var customers = _customerRepository.GetCustomers();
            return customers;
        }
        //Thêm mới khách hàng
        public ServiceResult AddCustomer(Customer customer)
        {
            var serviceResult = new ServiceResult();
            
            //validate dữ liệu
            //Check trường bắt buộc nhập, nếu dữ liệu chưa hợp lệ thì mô tả lỗi
            var customerCode = customer.CustomerCode;
            if (string.IsNullOrEmpty(customerCode))
            {
                var msg = new
                {
                    devMsg = new { fieldName = "CustomerCode", msg = "Ma khach hang khong duoc phep de trong" },
                    userMsg = "Mã khách hàng không được phép để trống",
                    Code = MISACode.NotValid, // Mã validate sai

                };
                serviceResult.MISACode = MISACode.NotValid;
                serviceResult.Messenger = "Mã khách hàng không được phép để trống";
                serviceResult.Data = msg;
                return serviceResult;
            }
                
            //check trùng mã:
            var res = _customerRepository.GetCustomerByCode(customerCode);
            if (res != null)
            {
                var msg = new
                {
                    devMsg = new { fieldName = "CustomerCode", msg = "Ma khach hang da ton tai" },
                    userMsg = "Mã khách hàng đã tồn tại",
                    Code = MISACode.NotValid, // Mã validate sai

                };
                serviceResult.MISACode = MISACode.NotValid;
                serviceResult.Messenger = "Mã khách hàng đã tồn tại";
                serviceResult.Data = msg;
                return serviceResult; 
            }
            
            //Thêm mới dữ liệu đã hợp lệ

            var rowAffects = _customerRepository.AddCustomer(customer);
            serviceResult.MISACode = MISACode.IsValid;
            serviceResult.Messenger = "Thêm thành công";
            serviceResult.Data = rowAffects;
            return serviceResult;
        }

        //Sửa thông tin khách hàng
        public ServiceResult UpdateCustomer(Customer customer)
        {
            var serviceResult = new ServiceResult();
           
            //validate dữ liệu
            //Check trường bắt buộc nhập, nếu dữ liệu chưa hợp lệ thì mô tả lỗi
            var customerCode = customer.CustomerCode;
            if (string.IsNullOrEmpty(customerCode))
            {
                var msg = new
                {
                    devMsg = new { fieldName = "CustomerCode", msg = "Ma khach hang chua ton tai" },
                    userMsg = "Mã khách hàng không tồn tại",
                    Code = MISACode.NotValid, // Mã validate sai

                };
                serviceResult.MISACode = MISACode.NotValid;
                serviceResult.Messenger = "Mã khách hàng không tồn tại";
                serviceResult.Data = msg;
                return serviceResult;
            }

            //check trùng mã:
            //Thêm mới dữ liệu đã hợp lệ

            var rowAffects = _customerRepository.UpdateCustomer(customer);
            serviceResult.MISACode = MISACode.IsValid;
            serviceResult.Messenger = "Thêm thành công";
            serviceResult.Data = rowAffects;
            return serviceResult;


        }
        //Xóa khách hàng theo khóa chính
        public ServiceResult DeleteCustomer(string customerCode)
        {
            var serviceResult = new ServiceResult();
           
            //validate dữ liệu
            //Check trường bắt buộc nhập, nếu dữ liệu chưa hợp lệ thì mô tả lỗi
            
            if (string.IsNullOrEmpty(customerCode))
            {
                var msg = new
                {
                    devMsg = new { fieldName = "CustomerCode", msg = "Ma khach hang khong duoc phep de trong" },
                    userMsg = "Mã khách hàng không được phép để trống",
                    Code = MISACode.NotValid, // Mã validate sai

                };
                serviceResult.MISACode = MISACode.NotValid;
                serviceResult.Messenger = "Mã khách hàng không được phép để trống";
                serviceResult.Data = msg;
                return serviceResult;
            }

            //check mã sai:
            var res = _customerRepository.GetCustomerByCode(customerCode);
            if (res == null)
            {
                var msg = new
                {
                    devMsg = new { fieldName = "CustomerCode", msg = "Ma khach khong ton tai" },
                    userMsg = "Mã khách hàng không tồn tại",
                    Code = MISACode.NotValid, // Mã validate sai

                };
                serviceResult.MISACode = MISACode.NotValid;
                serviceResult.Messenger = "Mã khách hàng không tồn tại";
                serviceResult.Data = msg;
                return serviceResult;
            }

            //Xóa dữ liệu đã hợp lệ

            var rowAffects = _customerRepository.DeleteCustomer(customerCode);
            serviceResult.MISACode = MISACode.IsValid;
            serviceResult.Messenger = "Xoá thành công";
            serviceResult.Data = rowAffects;
            return serviceResult;
        }

        public Customer GetCustomerByCode(string customerCode)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
