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
    public class RecruiterController : ControllerBase
    {
        #region Properties
        IRecruiterService _recruiterService;
        #endregion

        #region Constructors
        public RecruiterController(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }
        #endregion

        #region Public Actions
        [HttpGet("Get")]
        public IActionResult Get([FromQuery] Guid? pRecruiterId = null,
            [FromQuery] Guid? pCandidateId = null, [FromQuery] Guid? pClientId = null,
            [FromQuery] Guid? pRoomId = null, [FromQuery] Guid? pOpeningId = null,
            [FromQuery] bool WithDeleted = false)
        {
            var rs = new RecruiterResponse();
            try
            {
                var rq = new RecruiterRequest()
                {
                    Recruiter = new RecruiterModel()
                    {
                        Id = pRecruiterId
                    },
                    ClientId = pClientId,
                    CandidateId = pCandidateId,
                    RoomId = pRoomId,
                    OpeningId = pOpeningId,
                    WithDeleted = WithDeleted
                };

                return Ok(_recruiterService.GetRecruiters(rq));
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
        public IActionResult InsertUpdate([FromBody] RecruiterRequest rq)
        {
            var rs = new RecruiterResponse();
            try
            {
                return Ok(_recruiterService.InsertUpdateRecruiter(rq));
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
            var rs = new RecruiterResponse();
            try
            {
                var rq = new RecruiterRequest()
                {
                    Recruiter = new RecruiterModel()
                    {
                        Id = Id
                    }
                };

                if (_recruiterService.DeleteRecruiter(rq).Successfull)
                {
                    return Ok();
                }

                throw new Exception("Could not delete the Recruiter!");
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
