using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore;
using MISA.Entity.Model;
using MySql.Data.MySqlClient;

namespace MISA.CukCuk.Api.Api
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Lấy dữ liệu khách hàng không truyền tham số
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: ABC(11/1/2021)
        [HttpGet]
        public IActionResult Get()
        {
            var customerService = new CustomerService();
            var customers = customerService.GetCustomers();
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

            var customerService = new CustomerService();
            //Kết quả trở về
            var serviceResult = customerService.InsertCustomer(customer);
            //check du lieu
            if (serviceResult.MISACode == 900)
            {
                return BadRequest(serviceResult.Data);
            }
            if (serviceResult.MISACode == 100 && (int)serviceResult.Data > 0)
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
            //Kết nối tới máy chủ Database
            var connetionString = "Host=103.124.92.43; Port=3306; Database=MISACukCuk_MF663_DMGIANG; User Id=nvmanh; Password=12345678";
            //Khởi tạo đối tượng kết nối đến database
            IDbConnection dbConnection = new MySqlConnection(connetionString);

            DynamicParameters dynamicParameters = new DynamicParameters();
            var properties = customer.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(customer);
                if (property.PropertyType == typeof(Guid) || property.PropertyType == typeof(Guid?))
                {
                    propertyValue = propertyValue.ToString();
                }

                dynamicParameters.Add($"@{propertyName}", propertyValue);
            }


            //Dữ liệu vào database
            var customers = dbConnection.Execute("Proc_UpdateCustomer", commandType: CommandType.StoredProcedure, param: dynamicParameters);


            return Ok(customers);
        }

        [HttpDelete]
        public IActionResult Delete(Guid customerId)
        {
            //Kết nối tới Database 
            var connetionString = "Host=103.124.92.43; Port=3306; Database=MISACukCuk_MF663_DMGIANG; User Id=nvmanh; Password=12345678";
            IDbConnection dbConnection = new MySqlConnection(connetionString);
            //Dữ liệu từ database
            var customers = dbConnection.Execute($"DELETE FROM Customer WHERE CustomerId = '{customerId.ToString()}'");
            //Trả dữ liệu cho Client:
            return Ok(customers);
        }


    }
}