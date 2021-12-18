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
    public class CandidatesController : ControllerBase
    {
        #region Properties
        ICandidateService _candidateService;
        #endregion

        #region Constructors
        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }
        #endregion

        #region Public Actions
        [HttpGet("Get")]
        public IActionResult Get([FromQuery]Guid? pCandidateId = null,
            [FromQuery] Guid? pRecruiterId = null, [FromQuery] Guid? pRoomId = null,
            [FromQuery] Guid? pOpeningId = null, [FromQuery] Guid? pClientId = null,
            [FromQuery] bool WithDeleted = false)
        {
            var rs = new CandidateResponse();
            try
            {
                var rq = new CandidateRequest()
                {
                    Candidate = new CandidateModel()
                    {
                        Id = pCandidateId,
                    },
                    ClientId = pClientId,
                    RecruiterId = pRecruiterId,
                    OpeningId = pOpeningId,
                    RoomId = pRoomId,
                    WithDeleted = WithDeleted
                };

                return Ok(_candidateService.GetCandidates(rq));
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
        public IActionResult InsertUpdate([FromBody] CandidateRequest rq)
        {
            var rs = new CandidateResponse();
            try
            {
                return Ok(_candidateService.InsertUpdateCandidate(rq));
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
            var rs = new CandidateResponse();
            try
            {
                var rq = new CandidateRequest()
                {
                    Candidate = new CandidateModel()
                    {
                        Id = Id
                    }
                };

                if (_candidateService.DeleteCandidate(rq).Successfull)
                {
                    return Ok();
                }

                throw new Exception("Could not delete the Candidate!");
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
