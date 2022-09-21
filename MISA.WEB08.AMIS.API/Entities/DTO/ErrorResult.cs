using MISA.Web08.AMIS.API.Enums;

namespace MISA.Web08.AMIS.API.Entities.DTO
{
    /// <summary>
    /// class trả về lỗi tùy chỉnh
    /// </summary>
    /// Created by : TNMANH (21/09/2022)
    public class ErrorResult
    {
        #region Properties
        /// <summary>
        /// Mã lỗi enum
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Mã lỗi cho dev
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        public string DevMsg { get; set; }

        /// <summary>
        /// Mã lỗi cho người dùng
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        public string MoreInfo { get; set; }

        /// <summary>
        /// ID kết nối để trace sau này để dò lỗi
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        public string TraceId { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo class
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        /// <param name="devMsg">Tin nhắn gửi tới dev</param>
        /// <param name="userMsg">Tin nhắn gửi tới người dùng</param>
        /// <param name="moreInfo">Thông tin thêm</param>
        /// <param name="traceId">ID kết nối để dò lỗi sau này</param>
        /// Created by : TNMANH (21/09/2022)

        public ErrorResult(ErrorCode errorCode, string devMsg, string userMsg, string moreInfo, string traceId)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceId = traceId;
        }
        #endregion
    }
}
