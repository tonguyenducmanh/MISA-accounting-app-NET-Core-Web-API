namespace MISA.WEB08.AMIS.API.Entities
{
    /// <summary>
    /// Thực thể đơn vị map tới bảng Department trong Database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    public class Department
    {
        /// <summary>
        /// ID đơn vị
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Giới thiệu về đơn vị, phòng ban
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ngày tạo đơn vị
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo đơn vị
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
