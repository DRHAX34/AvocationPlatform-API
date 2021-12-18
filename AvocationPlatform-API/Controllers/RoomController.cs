using AvocationPlatform_Models.DataModels;
using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using AvocationPlatform_Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvocationPlatform_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        #region Properties
        IRoomService _roomService;
        #endregion

        #region Constructors
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        #endregion

        #region Public Actions
        [HttpGet("Get")]
        public IActionResult Get([FromQuery] Guid? RoomId, [FromQuery] Guid? CandidateId, [FromQuery] Guid? RecruiterId,
            [FromQuery] Guid? OpeningId, [FromQuery] Guid? ClientId, [FromQuery]bool WithDeleted = false)
        {
            var rs = new RoomResponse();
            try
            {
                var rq = new RoomRequest()
                {
                    Room = new RoomModel()
                    {
                        Id = RoomId
                    },
                    CandidateId = CandidateId,
                    RecruiterId = RecruiterId,
                    OpeningId = OpeningId,
                    ClientId = ClientId,
                    WithDeleted = WithDeleted
                };

                return Ok(_roomService.GetRooms(rq));
            }
            catch (Exception ex)
            {
                rs.Error.Status = AvocationPlatform_Models.Enumerations.ErrorStatus.Error;
                rs.Error.Message = ex.Message;
                rs.Error.HttpStatus = System.Net.HttpStatusCode.BadRequest;

                return BadRequest(rs);
            }
        }

        [HttpPost("InsertUpdate")]
        public IActionResult InsertUpdate([FromBody] RoomRequest rq)
        {
            var rs = new RoomResponse();
            try
            {
                return Ok(_roomService.InsertUpdateRoom(rq));
            }
            catch (Exception ex)
            {
                rs.Error.Status = AvocationPlatform_Models.Enumerations.ErrorStatus.Error;
                rs.Error.Message = ex.Message;
                rs.Error.HttpStatus = System.Net.HttpStatusCode.BadRequest;

                return BadRequest(rs);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] Guid Id)
        {
            var rs = new RoomResponse();
            try
            {
                var rq = new RoomRequest()
                {
                    Room = new RoomModel()
                    {
                        Id = Id
                    },
                    UserId = "TESTE" //TODO: Implement users
                };

                if (_roomService.DeleteRoom(rq).Successfull)
                {
                    return Ok();
                }

                throw new Exception("Could not delete the Room!");
            }
            catch (Exception ex)
            {
                rs.Error.Status = AvocationPlatform_Models.Enumerations.ErrorStatus.Error;
                rs.Error.Message = ex.Message;
                rs.Error.HttpStatus = System.Net.HttpStatusCode.BadRequest;

                return BadRequest(rs);
            }
        }
        #endregion
    }
}
