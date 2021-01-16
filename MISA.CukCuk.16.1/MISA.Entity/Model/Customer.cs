using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Entity.Model
{
    /// <summary>
    /// Class Khách hàng
    /// </summary>
    /// CreatedBy: ABC(11/01/2021)

    public class Customer
    {
        //Khai báo các field ko có set get
        #region Declare

        #endregion
        //Khai báo các hàng khởi tạo
        #region Constructor

        #endregion
        //Nơi chứa tất cả các property của Object
        #region Property 
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerId { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerCode { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Số thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }
        /// <summary>
        /// Số nợ
        /// </summary>
        public double? DebitAmount { get; set; }
        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Mã số thuế công ty
        /// </summary>
        public string CompanyTexCode { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } // string là chuỗi đặc biệt nên tự có định dạng trống ko cần thêm '?'
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính (0. nam, 1. nữ, 2. khác)
        /// </summary>
        public int? Gender { get; set; } //Cho phép trống có dạng: public(protected) định dạng? Name{get; set}
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Tạo bởi
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Sửa bởi
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion
        //Tất cả các phương thức
        #region Method

        #endregion

    }
}
