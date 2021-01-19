using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        #region DECLARE
        IConfiguration _configuration;
        #endregion
        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Method
        public int AddCustomer(Customer customer)
        {
            //Kết nối tới CSDL
            var connetionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            var dbConnection = new MySqlConnection(connetionString);
            var parameters = new DynamicParameters();
            var properties = customer.GetType().GetProperties();
            //Xử ly các kiểu dữ liệu
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(customer);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue.ToString());
                }
                else// ko co else luc nãy
                    parameters.Add($"@{propertyName}", propertyValue);
            }

            //Thực thi commandText
            //Dữ liệu vào database
            var rowAffects = dbConnection.Execute("Proc_InsertCustomer", param: parameters, commandType: CommandType.StoredProcedure);
            //Trả về số bản ghi (số bản ghi đã thêm ms)
            return rowAffects;
        }

        public int DeleteCustomer(string customerCode)
        {
            //Kết nối tới Database 
            var connetionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            IDbConnection dbConnection = new MySqlConnection(connetionString);
            ////Dữ liệu từ database
            var customers = dbConnection.Execute($"DELETE FROM Customer WHERE CustomerCode = '{customerCode.ToString()}'");
            ////Trả dữ liệu cho Client:
            return customers;
        }

        public Customer GetCustomerByCode(string customerCode)
        {
            var connetionString = "Host=103.124.92.43; Port=3306; Database=MISACukCuk_MF663_DMGIANG; User Id=nvmanh; Password=12345678";
            var dbConnection = new MySqlConnection(connetionString);
            var res = dbConnection.Query<Customer>($"SELECT * FROM Customer e WHERE e.CustomerCode = '{customerCode}' LIMIT 1").FirstOrDefault();
            return res;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            //Kết nối tới CSDL
            var connetionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            IDbConnection dbConnection = new MySqlConnection(connetionString);
            //Khởi tạo các commandText:
            var customers = dbConnection.Query<Customer>("SELECT * FROM Customer");
            //Trả về dữ liệu
            //Trả dữ liệu cho Client:
            return customers;
        }

        public int UpdateCustomer(Customer customer)
        {
            //Kết nối tới máy chủ Database
            var connetionString = _configuration.GetConnectionString("MISACukCukConnectionString");
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

            //Thực thi commandText
            //Dữ liệu vào database
            var rowAffects = dbConnection.Execute("Proc_UpdateCustomer", param: dynamicParameters, commandType: CommandType.StoredProcedure);
            //Trả về số bản ghi (số bản ghi đã sửa)
            return rowAffects;
        }

        #endregion

    }
}
