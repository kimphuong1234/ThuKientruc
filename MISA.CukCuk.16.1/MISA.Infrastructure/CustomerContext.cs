using Dapper;
using MISA.Entity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    public class CustomerContext
    {
        
        #region Method
        //Lấy danh sách khách hàng
        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: ABC(16/01/2021)
        public IEnumerable<Customer> GetCustomers()
        {
            //Kết nối tới CSDL
            var connetionString = "Host=103.124.92.43; Port=3306; Database=MISACukCuk_MF663_DMGIANG; User Id=nvmanh; Password=12345678";
            IDbConnection dbConnection = new MySqlConnection(connetionString);
            //Khởi tạo các commandText:
            var customers = dbConnection.Query<Customer>("SELECT * FROM Customer");
            //Trả về dữ liệu
            //Trả dữ liệu cho Client:
            return customers; 
        }
        //Lấy thông tin khách hàng theo mã khách hàng;

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Số bản ghi đc thêm mới</returns>
        /// CreatedBy: ABC(16/01/2021)
        public int InsertCustomer(Customer customer)
        {
            //Kết nối tới CSDL
            var connetionString = "Host=103.124.92.43; Port=3306; Database=MISACukCuk_MF663_DMGIANG; User Id=nvmanh; Password=12345678";
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
            var rowAffects = dbConnection.Execute("Proc_InsertCustomer",param: parameters, commandType: CommandType.StoredProcedure);
            //Trả về số bản ghi (số bản ghi đã thêm ms)
            return rowAffects;
        }
        /// <summary>
        /// Lấy khách hàng theo CustomerCode
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns>Khách hàng đầu tiên</returns>
        /// CreatedBy: ABC(16/01/2021)
        public Customer GetCustomerByCode(string customerCode)
        {
            var connetionString = "Host=103.124.92.43; Port=3306; Database=MISACukCuk_MF663_DMGIANG; User Id=nvmanh; Password=12345678";
            var dbConnection = new MySqlConnection(connetionString);
            var res = dbConnection.Query<Customer>($"SELECT * FROM Customer e WHERE e.CustomerCode = '{customerCode}' LIMIT 1").FirstOrDefault();
            return res;
        }
        //Sửa thông tin khách hàng
        public int UpdateCustomer(Customer customer)
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

            //Thực thi commandText
            //Dữ liệu vào databas
            var rowAffects = dbConnection.Execute("Proc_UpdateCustomer",param: dynamicParameters, commandType: CommandType.StoredProcedure);
            //Trả về số bản ghi (số bản ghi đã sửa)
            return rowAffects;
        }

   
        //Xóa khách hàng theo khóa chính
        public int Delete(string customerCode)
        {
            //Kết nối tới Database 
            var connetionString = "Host=103.124.92.43; Port=3306; Database=MISACukCuk_MF663_DMGIANG; User Id=nvmanh; Password=12345678";
            IDbConnection dbConnection = new MySqlConnection(connetionString);
            ////Dữ liệu từ database
            var customers = dbConnection.Execute($"DELETE FROM Customer WHERE CustomerCode = '{customerCode.ToString()}'");
            ////Trả dữ liệu cho Client:
            return customers;
        }
        #endregion
    }
}
