namespace MISA.Web08.AMIS.API.Enums
{
    /// <summary>
    /// enum error code của project này
    /// </summary>
    /// Created by : TNMANH (21/09/2022)
    public enum ErrorCode
    {
        /// <summary>
        /// Lỗi do exception
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        Exception = 1,

        /// <summary>
        /// Lỗi do trùng mã ID nhân viên
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        DuplicateCode = 2,

        /// <summary>
        /// Lỗi do mã để trống
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        EmptyCode = 3,

        /// <summary>
        /// Gọi vào DB để insert thất bại
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        InsertFailed = 4,

        /// <summary>
        /// Lỗi thuộc về database phòng ban
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        DepartmentError = 3,

        /// <summary>
        /// Lỗi thuộc về database chức vụ
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        PositionError = 4,
    }
}
