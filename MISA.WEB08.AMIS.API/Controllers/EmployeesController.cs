using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.API.Enums;
using MISA.Web08.AMIS.API.Entities.DTO;
using MISA.WEB08.AMIS.API.Entities;
using MISA.WEB08.AMIS.API.Entities.DTO;
using MISA.WEB08.AMIS.API.Enums;
using MySqlConnector;
using System.Text;
using System.Security.AccessControl;
using MISA.WEB08.AMIS.API.Resources;
using MISA.WEB08.AMIS.API.CustomAttribute;

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

        private readonly IConfiguration _configuration;


        /// <summary>
        /// Hàm khởi tạo để truyền configuration dùng để get connection string từ file
        /// appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        /// Created by : TNMANH (24/09/2022)
        #region Constructor

        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

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
                // Tạo connection
                var sqlConnection = new MySqlConnection(_configuration.GetConnectionString(MISAResource.ConnectionString));

                // chuẩn bị câu lệnh MySQL
                string storeProcedureName = MISAResource.ProcGetAllEmployee;


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
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
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
                // Tạo connection
                var sqlConnection = new MySqlConnection(_configuration.GetConnectionString(MISAResource.ConnectionString));

                // Chuẩn bị câu lệnh Query
                string storeProcedureName = MISAResource.ProcGetMaxEmployeeCode;

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
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
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
                // Tạo connection
                var sqlConnection = new MySqlConnection(_configuration.GetConnectionString(MISAResource.ConnectionString));

                // Khai báo procedure name
                string storeProcedureName = MISAResource.ProcGetEmployeeByID;

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
                    MISAResource.DevMsg_Exception, 
                    MISAResource.UserMsg_Exception, 
                    MISAResource.MoreInfo_Exception,
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
                // Tạo connection
                var sqlConnection = new MySqlConnection(_configuration.GetConnectionString(MISAResource.ConnectionString));

                // Chuẩn bị câu lệnh MySQL
                string storeProcedureName = MISAResource.ProcGetEmployeeFilter;

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
                    MISAResource.DevMsg_Exception, 
                    MISAResource.UserMsg_Exception, 
                    MISAResource.MoreInfo_Exception, 
                    HttpContext.TraceIdentifier));
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
                // Validate dữ liệu đầu vào
                var props = typeof(Employee).GetProperties();
                List<string> validateFailed = new List<string>();
                foreach(var prop in props)
                {
                    var propName = prop.Name;
                    var propValue = prop.GetValue(employee);
                    var mustHave = (MustHave?)Attribute.GetCustomAttribute(prop, typeof(MustHave));
                    if(mustHave != null && string.IsNullOrEmpty(propValue?.ToString()))
                    {
                        validateFailed.Add(mustHave.ErrorMessage);
                    }
                }

                // Check xem nếu có lỗi văng ra kết quả luôn khỏi chạy đoạn dưới
                if(validateFailed.Count > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        ErrorCode.EmptyCode,
                        MISAResource.DevMsg_ValidateFailed,
                        MISAResource.UserMsg_ValidateFailed,
                        validateFailed,
                        HttpContext.TraceIdentifier
                        ));
                }

                // Tạo connection
                var sqlConnection = new MySqlConnection(_configuration.GetConnectionString(MISAResource.ConnectionString));
                // Validate xem có bị trùng mã nhân viên không
                string testProcName = MISAResource.ProcGetDupplicateCode;
                DynamicParameters testParameters = new DynamicParameters();
                testParameters.Add("v_EmployeeCode", employee.EmployeeCode);
                // Thực hiện kiểm tra mã nhân viên trong database
                var testResult = sqlConnection.Query(
                        testProcName,
                        testParameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    );
                if(testResult.Count() > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        ErrorCode.DuplicateCode,
                        MISAResource.DevMsg_DuplicatedCode,
                        MISAResource.UserMsg_DuplicatedCode,
                        MISAResource.MoreInfo_DupplicatedCode,
                        HttpContext.TraceIdentifier
                        ));
                }
                else
                {

                // Tạo ra employeeID bằng guid
                Guid employeeID = Guid.NewGuid();

                // chuẩn bị câu lệnh MySQL
                string storeProcedureName = MISAResource.ProcPostNewEmployee;

                // Truyền tham số vào store procedure
                DynamicParameters parameters = new DynamicParameters();

                // Chèn các giá trị khác vào param cho store procedure
                foreach (var prop in props)
                {
                    // lấy ra tên của properties
                    var propName = prop.Name;
                    var propValue = prop.GetValue(employee);
                    parameters.Add($"v_{propName}", propValue);
                }

                parameters.Add("v_EmployeeID", employeeID);

                // Thực hiện chèn dữ liệu vào trong database
                var queryResult = sqlConnection.Execute(
                        storeProcedureName,
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                // Trả về kết quả
                return StatusCode(StatusCodes.Status200OK, queryResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception,
                    MISAResource.MoreInfo_Exception,
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

            try
            {
                // Validate dữ liệu đầu vào
                var props = typeof(Employee).GetProperties();
                List<string> validateFailed = new List<string>();
                foreach (var prop in props)
                {
                    var propName = prop.Name;
                    var propValue = prop.GetValue(employee);
                    var mustHave = (MustHave?)Attribute.GetCustomAttribute(prop, typeof(MustHave));
                    if (mustHave != null && string.IsNullOrEmpty(propValue?.ToString()))
                    {
                        validateFailed.Add(mustHave.ErrorMessage);
                    }
                }

                // Check xem nếu có lỗi văng ra kết quả luôn khỏi chạy đoạn dưới
                if (validateFailed.Count > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        ErrorCode.EmptyCode,
                        MISAResource.DevMsg_ValidateFailed,
                        MISAResource.UserMsg_ValidateFailed,
                        validateFailed,
                        HttpContext.TraceIdentifier
                        ));
                }
                // Tạo connection
                var sqlConnection = new MySqlConnection(_configuration.GetConnectionString(MISAResource.ConnectionString));

                // chuẩn bị câu lệnh MySQL
                string storeProcedureName = MISAResource.ProcPutOneEmployee;

                // Truyền tham số vào store procedure
                DynamicParameters parameters = new DynamicParameters();

                // Chèn các giá trị khác vào param cho store procedure
                foreach (var prop in props)
                {
                    // lấy ra tên của properties
                    var propName = prop.Name;
                    var propValue = prop.GetValue(employee);
                    parameters.Add($"v_{propName}", propValue);
                }

                // Thực hiện chèn dữ liệu vào trong database
                var queryResult = sqlConnection.Execute(
                        storeProcedureName,
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                // Trả về kết quả
                return StatusCode(StatusCodes.Status200OK, queryResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    MISAResource.DevMsg_Exception,
                    MISAResource.UserMsg_Exception, 
                    MISAResource.MoreInfo_Exception,
                     HttpContext.TraceIdentifier
                ));
            }
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
                // Tạo connection
                var sqlConnection = new MySqlConnection(_configuration.GetConnectionString(MISAResource.ConnectionString));

                // khởi tạo store procedure
                string storeProcedureName = MISAResource.ProcDeleteOneEmployee;

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
