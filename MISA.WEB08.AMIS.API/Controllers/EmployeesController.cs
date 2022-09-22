using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.API.Enums;
using MISA.Web08.AMIS.API.Entities.DTO;
using MISA.WEB08.AMIS.API.Entities;
using MISA.WEB08.AMIS.API.Entities.DTO;
using MISA.WEB08.AMIS.API.Enums;
using MySqlConnector;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// Danh sách các API liên quan tới dữ liệu nhân viên của bảng employee trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpGet("")]
        public IActionResult GetAllEmployees()
        {

            try
            {
                // Tạo connection string
                string connectionString = "" +
                    "Server = localhost;" +
                    "Port = 5060;" +
                    "Database = misa.web08.gpbl.tnmanh;" +
                    "User Id = root;" +
                    "Password = 140300;";
                var sqlConnection = new MySqlConnection(connectionString);

                // chuẩn bị câu lệnh MySQL
                string storeProcedureName = "Proc_employee_GetAll";


                // thực hiện gọi vào DB
                var employees = sqlConnection.Query<Employee>(
                    storeProcedureName
                    , commandType: System.Data.CommandType.StoredProcedure
                    );


                return StatusCode(StatusCodes.Status200OK, employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                   "It was not posible to connect to the redis server(s)",
                    "Có lỗi xảy ra, vui lòng liên hệ với MISA.",
                    "https://openapi.misa.com.vn/errorcode/e001",
                     HttpContext.TraceIdentifier
                ));
            }
        }

        /// <summary>
        /// API lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpGet("max-code")]
        public IActionResult GetMaxEmployeeCode()
        {
            try
            {
                // Tạo connection string
                string connectionString = "" +
                "Server = localhost;" +
                "Port = 5060;" +
                "Database = misa.web08.gpbl.tnmanh;" +
                "User Id = root;" +
                "Password = 140300;";

                var sqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh Query
                string storeProcedureName = "Proc_employee_GetMaxCode";

                // Thực hiện gọi vào Database
                var maxCode = sqlConnection.QueryFirstOrDefault<String>(
                    storeProcedureName,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                // Trả về Status code và kết quả
                return StatusCode(StatusCodes.Status200OK, maxCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về Status code và object báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    "Has error for this request",
                    "Có lỗi xảy ra, vui lòng liên hệ với MISA",
                    "https://openapi.google.com/api/errorcode/e001",
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        /// <summary>
        /// API lấy thông tin chi tiết của 1 nhân viên theo ID đầu vào
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Thông tin của nhân viên theo ID</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpGet("{employeeID}")]
        public IActionResult GetEmployeeByID([FromRoute] Guid employeeID)
        {
            try
            {
                // Tạo connection String
                string connectionString = "" +
                    "Server = localhost;" +
                    "Port = 5060;" +
                    "Database = misa.web08.gpbl.tnmanh;" +
                    "User Id = root;" +
                    "Password = 140300;";
                var sqlConnection = new MySqlConnection(connectionString);

                // Khai báo procedure name
                string storeProcedureName = "Proc_employee_GetOne";

                // Khởi tạo các parameter để chèn vào trong storeprocedure
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("v_id", employeeID);

                // Thực hiện kết nối tới Database
                var employee = sqlConnection.QueryFirstOrDefault<Employee>(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                // Trả về status code và kết quả trả về
                return StatusCode(StatusCodes.Status200OK, employee);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code kèm theo kết quả báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    "Has error with server",
                    "Có lỗi xảy ra, vui lòng liên hệ với MISA",
                    "https://openapi.google.com/api/error-code/e001",
                    HttpContext.TraceIdentifier
                    ));
            }

        }

        /// <summary>
        /// API lọc danh sách nhân viên theo các điều kiện cho trước
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã, tên, số điện thoại của nhân viên)</param>
        /// <param name="limit">Số lượng kết quả trả về của 1 bảng</param>
        /// <param name="offset">Start Index của bảng</param>
        /// <returns>Tổng số bản ghi, tổng số trang, số trang hiện tại, danh sách kết quả</returns>
        [HttpGet("filter")]
        public IActionResult FilterEmployee(
            [FromQuery] string? keyword,
            [FromQuery] int? limit,
            [FromQuery] int? offset
            )
        {
            try
            {
                // Tạo connection string
                string connectionString = "" +
                    "Server = localhost;" +
                    "Port = 5060;" +
                    "Database = misa.web08.gpbl.tnmanh;" +
                    "User Id = root;" +
                    "Password = 140300;";
                var sqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh MySQL
                string storeProcedureName = "Proc_employee_GetPaging";

                // Chèn parameter cho procedure
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("v_Offset", offset);
                parameters.Add("v_Limit", limit);
                parameters.Add("v_Search", keyword);

                // Thực hiện gọi vào trong Database
                var employeesFiltered = sqlConnection.QueryMultiple(
                        storeProcedureName,
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                // Trả về status code kèm theo object kết quả
                return StatusCode(StatusCodes.Status200OK, new PagingData()
                {
                    PageSize = limit,
                    PageNumber = offset / limit + 1,
                    Data = employeesFiltered.Read<Employee>().ToList(),
                    TotalCount = unchecked((int)employeesFiltered.ReadSingle().TotalCount),
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code kèm theo object thông báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    "Has error with server.",
                    "Có lỗi xảy ra, vui lòng liên hệ với MISA để biết thêm chi tiết.",
                    "Https://openapi.google.com/api/error-code/e001",
                    HttpContext.TraceIdentifier
                    ));
            }

        }

        #endregion

        // Danh sách các API liên quan tới việc tạo mới nhân viên
        #region PostMethod

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên mới</param>
        /// <returns>Status 201 created, employeeID</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                // khởi tạo kết nối với DB MySQL
                string connectionString = "" +
                    "Server = localhost;" +
                    "Port = 5060;" +
                    "Database = misa.web08.gpbl.tnmanh;" +
                    "User Id = root;" +
                    "Password = 140300;";
                var sqlConnection = new MySqlConnection(connectionString);

                // khai báo tên procedure Insert
                var storeProcedureName = "Prop_employee_Insert";
                // chuẩn bị tham số đầu vào cho procedure
                var parameters = new DynamicParameters();
                var tempEmployeeInsertID = Guid.NewGuid();
                parameters.Add("v_EmployeeID", tempEmployeeInsertID);
                parameters.Add("v_EmployeeCode", employee.EmployeeCode);
                // Thực hiện gọi vào db để chạy procedure
                var numberOfAffectedRows = sqlConnection.Execute(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );
                // xử lý dữ liệu trả về
                if (numberOfAffectedRows > 0)
                {
                    // thành công
                    return StatusCode(StatusCodes.Status201Created, tempEmployeeInsertID);
                }
                else
                {
                    // thất bại
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                (
                    ErrorCode.InsertFailed,
                   "Insert to database return 0",
                    "Thêm mới nhân viên thất bại",
                    "https://openapi.misa.com.vn/errorcode/e001",
                     HttpContext.TraceIdentifier
                ));
                }

                return StatusCode(StatusCodes.Status201Created, employee.EmployeeID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                   "It was not posible to connect to the redis server(s)",
                    "Có lỗi xảy ra, vui lòng liên hệ với MISA.",
                    "https://openapi.misa.com.vn/errorcode/e001",
                     HttpContext.TraceIdentifier
                ));
            }
        }

        #endregion

        #region PutMethod

        /// <summary>
        /// API sửa thông tin của 1 nhân viên dựa vào employeeID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên định sửa</param>
        /// <param name="employee">Giá trị sửa</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpPut("{employeeID}")]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            return StatusCode(StatusCodes.Status200OK, employeeID);
        }

        #endregion

        #region DeleteMethod

        /// <summary>
        /// API xóa 1 nhân viên dựa vào ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpDelete("{employeeID}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeID)
        {
            try
            {
                // Tạo connection string
                string connectionString = "" +
                    "Server = localhost;" +
                    "Port = 5060;" +
                    "Database = misa.web08.gpbl.tnmanh;" +
                    "User Id = root;" +
                    "Password = 140300;";
                var sqlConnection = new MySqlConnection(connectionString);

                // khởi tạo store procedure
                string storeProcedureName = "Proc_employee_DeleteOne";

                // khởi tạo các parameter truyền vào trong store procedure
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("v_id", employeeID);

                // thực hiện truy vấn tới database
                var deleteOne = sqlConnection.Execute(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                // trả về status code và kết quả
                return StatusCode(StatusCodes.Status200OK, deleteOne);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // trả về status code và object báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    "Has error with server",
                    "Có lỗi xảy ra, vui lòng liên hệ với quản trị viên MISA.",
                    "https://openapi.google.com/api/error-code/e001",
                    HttpContext.TraceIdentifier

                    ));
            }
        }

        #endregion
    }
}
