using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.API.Entities.DTO;
using MISA.Web08.AMIS.API.Enums;
using MISA.WEB08.AMIS.API.Entities;
using MISA.WEB08.AMIS.API.Resources;
using MySqlConnector;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// Các api liên quan tới việc lấy dữ liệu chức vụ từ bảng positions trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Hàm khởi tạo để gọi configuration để lấy được connection string
        /// </summary>
        /// <param name="configuration"></param>
        /// Created by : TNMANH (24/09/2022)
        #region Contructor

        public PositionsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region method GET
        /// <summary>
        /// Lấy danh sách tất cả các chức vụ
        /// </summary>
        /// Created by : TNMANH (17/09/2022)
        /// <returns>Danh sách tất cả chức vụ</returns>
        [HttpGet]
        [Route("")]
        public IActionResult GetAllPositions()
        {
            try
            {
                // Tạo ra connection
                var sqlConnection = new MySqlConnection(_configuration.GetConnectionString("SecretConnectionString"));

                // Chuẩn bị câu lệnh MySQL
                string storeProcedureName = "Proc_positions_GetAll";

                // Thực hiện gọi vào Database
                var positions = sqlConnection.Query<Positions>(
                    storeProcedureName,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                // Trả về status code và mảng kết quả
                return StatusCode(StatusCodes.Status200OK, positions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status lỗi kèm theo object thông báo lỗi ErrorResult
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                    ));
            }
        } 
        #endregion
    }
}
