using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore;
using MISA.ApplicationCore.Interface;
using MISA.Entity;
using MISA.Entity.Model;
using MySql.Data.MySqlClient;

namespace MISA.CukCuk.Api.Api
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        /// <summary>
        /// Lấy dữ liệu khách hàng không truyền tham số
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: ABC(11/1/2021)
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerService.GetCustomers(); //Gọi qua interface
            return Ok(customers);
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo Id => có truyền tham số
        /// </summary>
        /// <param name="Id">Id của khách hàng</param>
        /// <returns></returns>
        /// CreatedBy: ABC(11/01/2021)
        [HttpGet("{customerId}")]
        public IActionResult GetCustomer(Guid customerId)
        {
            //Kết nối tới Database 
            var connetionString = "Host=103.124.92.43; Port=3306; Database=MISACukCuk_MF663_DMGIANG; User Id=nvmanh; Password=12345678";
            IDbConnection dbConnection = new MySqlConnection(connetionString);
            //Dữ liệu từ database
            var customers = dbConnection.Query<Customer>($"SELECT * FROM Customer WHERE CustomerId = '{customerId.ToString()}'").FirstOrDefault();
            //Trả dữ liệu cho Client:
            return Ok(customers);
        }

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// CreatedBy: ABC(11/01/2021)
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            #region
            //Video 16                 <------------
            //var properties = customer.GetType().GetProperties();                 <------------
            //var parameters = new DynamicParameters();
            //foreach(var property in properties)
            //{
            //    var propertyName = property.Name;
            //    var propertyValue = property.GetValue(customer);
            //    var propertyType = property.PropertyType;
            //    if(propertyType == typeof(Guid))
            //    {
            //        parameters.Add($"@(propertyName)", propertyValue, DbType.String);
            //    }
            //    else
            //    {
            //        parameters.Add($"@(propertyName)", propertyValue);
            //    }
            //}


            //Video Rec 01-11-21 phút 30
            //var storeParamObject = new                <------------
            //{
            //    CustomerId = customer.CustomerId.ToString(connetionString),
            //    CustomerCode = customer.CustomerCode,
            //    FullName = customer.FullName,
            //    PhoneNumber = customer.PhoneNumber,
            //    Email = customer.Email,
            //    CustomerGroupId = customer.CustomerGroupId.ToString()
            //}; 
            //Dữ liệu vào database
            //var customers = dbConnection.Execute("Proc_InsertCustomer", commandType: CommandType.StoredProcedure, param: storeParamObject);
            //return Ok(customers); ------> Cách vẫn đang lỗi


            //Video Rec 01-11-21 phút 60                 <------------
            //DynamicParameters dynamicParameters = new DynamicParameters();
            //dynamicParameters.Add("@CustomerId", customer.CustomerId.ToString());
            //dynamicParameters.Add("@CustomerCode", customer.CustomerCode);
            //dynamicParameters.Add("@FullName", customer.FullName);
            //dynamicParameters.Add("@PhoneNumber", customer.PhoneNumber);
            //dynamicParameters.Add("@Email", customer.Email);
            //dynamicParameters.Add("@CustomerGroupId", customer.CustomerGroupId.ToString());
            //Dữ liệu vào database
            //var customers = dbConnection.Execute("Proc_InsertCustomer",commandType:CommandType.StoredProcedure, param: dynamicParameters);
            //return Ok(customers);

            #endregion


            //Kết quả trở về
            var serviceResult = _customerService.AddCustomer(customer);
            //check du lieu
            if (serviceResult.MISACode == MISACode.NotValid)
            {
                return BadRequest(serviceResult.Data);
            }
            if (serviceResult.MISACode == MISACode.IsValid && (int)serviceResult.Data > 0)
            {
                return Created("abc", customer);
            }
            else
                return NoContent();
            //Dữ liệu vào database
            
        }

        [HttpPut]
        public IActionResult Put([FromBody] Customer customer) //update
        {
            //Kết quả trở về
            var serviceResult = _customerService.UpdateCustomer(customer);
            //check du lieu
            if (serviceResult.MISACode == MISACode.NotValid)
            {
                return BadRequest(serviceResult.Data);
            }
            if (serviceResult.MISACode == MISACode.IsValid && (int)serviceResult.Data > 0)
            {
                return Created("abc", customer);
            }
            else
                return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(string customerCode)
        {
           
            //Kết quả trở về
            var serviceResult = _customerService.DeleteCustomer(customerCode);
            //check du lieu
            if (serviceResult.MISACode == MISACode.NotValid)
            {
                return BadRequest(serviceResult.Data);
            }
            if (serviceResult.MISACode == MISACode.IsValid && (int)serviceResult.Data > 0)
            {
                return Delete(customerCode);
            }
            else
                return NoContent();
        }


    }
}