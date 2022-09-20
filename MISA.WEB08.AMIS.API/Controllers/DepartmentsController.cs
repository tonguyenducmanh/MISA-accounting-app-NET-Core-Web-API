using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.API.Entities;

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
            return StatusCode(StatusCodes.Status200OK, new List<Department>
            {
                new Department
                {
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "D001",
                    DepartmentName = "Phòng hành chính",
                    Description = "Đây là phần mô tả của phòng hành chính",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Hải Long",
                    ModifiedDate  = DateTime.Now,
                    ModifiedBy = "Tô Nguyễn Đức Mạnh",
                },
                new Department
                {
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "D002",
                    DepartmentName = "Phòng nhân sự",
                    Description = "Đây là phần mô tả của phòng nhân sự",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Hải Long",
                    ModifiedDate  = DateTime.Now,
                    ModifiedBy = "Nguyễn Hải Nam",
                }
            });
        } 
        #endregion
    }
}
