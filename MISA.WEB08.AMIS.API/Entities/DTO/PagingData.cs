namespace MISA.WEB08.AMIS.API.Entities.DTO
{
    /// <summary>
    /// DTO paging data
    /// </summary>
    /// Created: TNMANH (17/09/2022)
    public class PagingData
    {
        #region Properties

        /// <summary>
        /// Tổng số bảng ghi phù hợp kết quả filter
        /// </summary>
        public int? TotalCount { get; set; }

        /// <summary>
        /// Số lượng bản ghi trên 1 trang
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Số của trang hiện tại
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Danh sách nhân viên thỏa mãn điều kiện tìm kiếm
        /// </summary>
        public List<Employee> Data { get; set; } 

        #endregion
    }
}
