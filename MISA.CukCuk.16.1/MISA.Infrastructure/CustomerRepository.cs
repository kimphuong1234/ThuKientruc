using Dapper;
using MISA.Entity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        public int AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public int DeleteCustomer(string customerCode)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByCode(string customerCode)
        {
            throw new NotImplementedException();
        }

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

        public int UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
