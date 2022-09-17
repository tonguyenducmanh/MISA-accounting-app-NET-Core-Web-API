namespace MISA.WEB08.AMIS.API.Entities
{
    /// <summary>
    /// Thực thể chức danh map với bảng Positions trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    public class Position
    {
        /// <summary>
        /// ID chức danh
        /// </summary>
        public Guid PositionID { get; set; }

        /// <summary>
        /// Mã chức danh
        /// </summary>
        public string PositionCode { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Giới thiệu về chức danh
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ngày tạo chức danh
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo chức danh
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
