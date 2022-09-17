namespace MISA.WEB08.AMIS.API.Resources
{
    /// <summary>
    /// Tổng hợp các đoạn văn bản thông báo tới người dùng
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    public class MISAResources
    {
        public string MISAException (Exception error)
        {
            return $"Có lỗi xảy ra, vui lòng liên hệ với quản trị viên: {error}";
        }
    }
}
