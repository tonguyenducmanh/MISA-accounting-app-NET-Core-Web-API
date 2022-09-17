using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.API.Entities;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// Các api liên quan tới việc lấy dữ liệu chức vụ từ bảng positions trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        /// <summary>
        /// Lấy danh sách tất cả các chức vụ
        /// </summary>
        /// Created by : TNMANH (17/09/2022)
        /// <returns>Danh sách tất cả chức vụ</returns>
        [HttpGet]
        [Route("")]
        public List<Position> GetAllPositions()
        {
            return new List<Position>
            {
                new Position
                {
                    PositionID = Guid.NewGuid(),
                    PositionCode = "P001",
                    PositionName = "Giám đốc",
                    Description = "Đây là mô tả về vị trí giám đốc",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Liễu Thị Oanh",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Trần Xuân Bảo",
                },
                new Position
                {
                    PositionID = Guid.NewGuid(),
                    PositionCode = "P002",
                    PositionName = "Chủ tịch",
                    Description = "Đây là mô tả về vị trí chủ tịch",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Liễu Thị Oanh",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Phạm Thị Phương",
                },
                new Position
                {
                    PositionID = Guid.NewGuid(),
                    PositionCode = "P003",
                    PositionName = "Trưởng phòng",
                    Description = "Đây là mô tả về vị trí trưởng phòng",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Liễu Thị Oanh",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Phạm Thị Phương",
                }
            };
        }
    }
}
