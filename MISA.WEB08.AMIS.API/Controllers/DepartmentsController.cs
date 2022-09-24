using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.API.Entities.DTO;
using MISA.Web08.AMIS.API.Enums;
using MISA.WEB08.AMIS.API.Entities;
using MySqlConnector;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// Các API liên quan tới việc lấy dữ liệu của bảng đơn vị trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Hàm khởi tạo để gọi configuration để lấy được connection string từ bên appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        /// Created by : TNMANH (24/09/2022)
        #region Contructor

        public DepartmentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion


        #region method GET
        /// <summary>
        /// API lấy danh sách toàn bộ đơn vị
        /// </summary>
        /// <returns>Danh sách đơn vị</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpGet]
        [Route("")]
        public IActionResult GetAllDepartments()
        {
            try
            {
                // Tạo connection
                var sqlConnection = new MySqlConnection(_configuration.GetConnectionString("SecretConnectionString"));

                // Chuẩn bị câu lệnh MySQL
                string storeProcedureName = "Proc_department_GetAll";

                // Thực hiện gọi vào Database
                var departments = sqlConnection.Query<Department>(
                    storeProcedureName,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                // Trả về status code và mảng kết quả
                return StatusCode(StatusCodes.Status200OK, departments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status lỗi kèm theo object thông báo lỗi ErrorResult
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    (
                    ErrorCode.Exception,
                    "Has error when try to request to server.",
                    "Có lỗi xảy ra, vui lòng liên hệ với MISA.",
                    "https://openapi.google.com/errorcode/e001",
                    HttpContext.TraceIdentifier
                    ));
            }

        }
        #endregion
    }
}
