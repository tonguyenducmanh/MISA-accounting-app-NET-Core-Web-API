﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.AMIS.API.Entities.DTO;
using MISA.Web08.AMIS.API.Enums;
using MISA.WEB08.AMIS.API.Entities;
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
                // Tạo ra connection string
                string connectionString = "" +
                       "Server = localhost;" +
                       "Port = 5060;" +
                       "Database = misa.web08.gpbl.tnmanh;" +
                       "User Id = root;" +
                       "Password = 140300;";
                var sqlConnection = new MySqlConnection(connectionString);

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
                    "Has error when try to request to server.",
                    "Có lỗi xảy ra, vui lòng liên hệ với MISA",
                    "https://openapu.google.com/errorcode/e001",
                    HttpContext.TraceIdentifier
                    ));
            }
        } 
        #endregion
    }
}
