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
            return StatusCode(StatusCodes.Status200OK, new Employee
            {
                EmployeeID = employeeID,
                EmployeeCode = "NV001",
                FullName = "Tô Nguyễn Đức Mạnh",
                DateOfBirth = DateTime.Now,
                Gender = Gender.Male,
                EmployeeType = EmployeeType.Customer,
                IdentityCard = "034200007684",
                IdentityPlace = "CA Thái Bình",
                Address = "Tổ dân phố Trung Tiến, Thị trấn Tiền Hải, Tiền Hải, Thái Bình",
                PhoneNumberRelative = "00981071321",
                PhoneNumberFix = "19001001",
                Email = "Ducmanh1403200@gmail.com",
                BankAccount = "1201012011",
                BankName = "BIDV",
                BankBranch = "Chi nhánh Cầu Giấy",
                DepartmentID = Guid.NewGuid(),
                DepartmentName = "Phòng Công Nghệ Thông Tin",
                PositionID = Guid.NewGuid(),
                PositionName = "Giám đốc công nghệ",
                CreatedDate = DateTime.Now,
                CreatedBy = "Liễu Thị Oanh",
                ModifiedDate = DateTime.Now,
                ModifiedBy = "Nguyễn Hải Nam",
            });
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
            return StatusCode(StatusCodes.Status200OK, new PagingData
            {
                TotalCount = 100,
                PageSize = limit,
                PageNumber = 1,
                Data = new List<Employee>
                {
                    new Employee
                    {
                        EmployeeID = Guid.NewGuid(),
                        EmployeeCode = "NV001",
                        FullName = "Tô Nguyễn Đức Mạnh",
                        DateOfBirth = DateTime.Now,
                        Gender = Gender.Male,
                        EmployeeType = EmployeeType.Customer,
                        IdentityCard = "034200007684",
                        IdentityPlace = "CA Thái Bình",
                        Address = "Tổ dân phố Trung Tiến, Thị trấn Tiền Hải, Tiền Hải, Thái Bình",
                        PhoneNumberRelative = "00981071321",
                        PhoneNumberFix = "19001001",
                        Email = "Ducmanh1403200@gmail.com",
                        BankAccount = "1201012011",
                        BankName = "BIDV",
                        BankBranch = "Chi nhánh Cầu Giấy",
                        DepartmentID = Guid.NewGuid(),
                        DepartmentName = "Phòng Công Nghệ Thông Tin",
                        PositionID = Guid.NewGuid(),
                        PositionName = "Giám đốc công nghệ",
                        CreatedDate = DateTime.Now,
                        CreatedBy = "Liễu Thị Oanh",
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = "Nguyễn Hải Nam",
                    }
                }
            });
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
            return StatusCode(StatusCodes.Status200OK, employeeID);
        }

        #endregion
    }
}
