using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.API.Entities;
using MISA.WEB08.AMIS.API.Entities.DTO;
using MISA.WEB08.AMIS.API.Enums;
using MISA.WEB08.AMIS.API.Resources;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// Danh sách các API liên quan tới dữ liệu nhân viên của bảng employee trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    [Route("api/[controller]")]
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
        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    EmployeeID = Guid.NewGuid(),
                    EmployeeCode = "NV001",
                    FullName = "Tô Nguyễn Đức Mạnh",
                    DateOfBirth = DateTime.Now,
                    EmployeeGender = Gender.Male,
                    EmployeeType = EmployeeType.Customer,
                    IdentityCard = "034200007684",
                    IdentityPlace = "CA Thái Bình",
                    Address = "Tổ dân phố Trung Tiến, Thị trấn Tiền Hải, Tiền Hải, Thái Bình",
                    PNumRelative = "00981071321",
                    PNumFix = "19001001",
                    Email = "Ducmanh1403200@gmail.com",
                    BankAccount = "1201012011",
                    BankName ="BIDV",
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
            };
        }

        /// <summary>
        /// API lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpGet("/maxEmployeeCode")]
        public string GetMaxEmployeeCode()
        {
            return "NV99999";
        }

        /// <summary>
        /// API lấy thông tin chi tiết của 1 nhân viên theo ID đầu vào
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Thông tin của nhân viên theo ID</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpGet("{employeeID}")]
        public Employee GetEmployeeByID([FromRoute] Guid employeeID)
        {
            return new Employee
                {
                    EmployeeID = employeeID,
                    EmployeeCode = "NV001",
                    FullName = "Tô Nguyễn Đức Mạnh",
                    DateOfBirth = DateTime.Now,
                    EmployeeGender = Gender.Male,
                    EmployeeType = EmployeeType.Customer,
                    IdentityCard = "034200007684",
                    IdentityPlace = "CA Thái Bình",
                    Address = "Tổ dân phố Trung Tiến, Thị trấn Tiền Hải, Tiền Hải, Thái Bình",
                    PNumRelative = "00981071321",
                    PNumFix = "19001001",
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
                };
        }

        /// <summary>
        /// API lọc danh sách nhân viên theo các điều kiện cho trước
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã, tên, số điện thoại của nhân viên)</param>
        /// <param name="limit">Số lượng kết quả trả về của 1 bảng</param>
        /// <param name="offset">Start Index của bảng</param>
        /// <returns>Tổng số bản ghi, tổng số trang, số trang hiện tại, danh sách kết quả</returns>
        [HttpGet("filter")]
        public PagingData FilterEmployee(
            [FromQuery] string? keyword,
            [FromQuery] int? limit,
            [FromQuery] int? offset
            )
        {
            return new PagingData
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
                        EmployeeGender = Gender.Male,
                        EmployeeType = EmployeeType.Customer,
                        IdentityCard = "034200007684",
                        IdentityPlace = "CA Thái Bình",
                        Address = "Tổ dân phố Trung Tiến, Thị trấn Tiền Hải, Tiền Hải, Thái Bình",
                        PNumRelative = "00981071321",
                        PNumFix = "19001001",
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
            };
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
            return StatusCode(StatusCodes.Status201Created, employee.EmployeeID);
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
                return StatusCode(StatusCodes.Status200OK, employeeID);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, employeeID);
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
                return StatusCode(StatusCodes.Status200OK, employeeID);
            }
            catch
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed, employeeID);
            }
        }

        #endregion
    }
}
