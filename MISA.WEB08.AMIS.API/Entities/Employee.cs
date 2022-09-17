using MISA.WEB08.AMIS.API.Enums;

namespace MISA.WEB08.AMIS.API.Entities
{
    /// <summary>
    /// Thực thể nhân viên map tới bảng Employee trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    public class Employee
    {
        /// <summary>
        /// ID của nhân viên
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã của nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên của nhân viên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh của nhân viên
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính của nhân viên
        /// </summary>
        public Gender EmployeeGender { get; set; }

        /// <summary>
        /// Loại nhân viên
        /// </summary>
        public EmployeeType EmployeeType { get; set; }

        /// <summary>
        /// Căn cước công dân / chứng minh thư
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// Nơi cấp căn cước công dân / chứng minh thư
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Địa chỉ của nhân viên
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số điện thoại di động của nhân viên
        /// </summary>
        public string PNumRelative { get; set; }

        /// <summary>
        /// Số điện thoại cố định của nhân viên
        /// </summary>
        public string PNumFix { get; set; }

        /// <summary>
        /// Địa chỉ Email của nhân viên
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng của nhân viên
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh của ngân hàng
        /// </summary>
        public string BankBranch { get; set; }

        /// <summary>
        /// ID của đơn vị
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Tên của đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// ID của chức danh
        /// </summary>
        public Guid PositionID { get; set;   }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Ngày tạo nhân viên
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo nhân viên
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
