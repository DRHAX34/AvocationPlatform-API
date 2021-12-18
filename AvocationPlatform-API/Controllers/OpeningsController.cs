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
    public class OpeningsController : ControllerBase
    {
        #region Properties
        IOpeningService _openingService;
        #endregion

        #region Constructors
        public OpeningsController(IOpeningService openingService)
        {
            _openingService = openingService;
        }
        #endregion

        #region Public Actions
        [HttpGet("Get")]
        public IActionResult Get([FromQuery] Guid? pOpeningId = null,
            [FromQuery] Guid? pRecruiterId = null, [FromQuery] Guid? pClientId = null,
            [FromQuery] Guid? pCandidateId = null, [FromQuery] bool WithDeleted = false)
        {
            var rs = new OpeningResponse();
            try
            {
                var rq = new OpeningRequest()
                {
                    Opening = new OpeningModel()
                    {
                        Id = pOpeningId,
                        ClientId = pClientId
                    },
                    CandidateId = pCandidateId,
                    ClientId = pClientId,
                    RecruiterId = pRecruiterId,
                    WithDeleted = WithDeleted
                };
                return Ok(_openingService.GetOpenings(rq));
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
        public IActionResult InsertUpdate([FromBody] OpeningRequest rq)
        {
            var rs = new OpeningResponse();
            try
            {
                return Ok(_openingService.InsertUpdateOpening(rq));
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
            var rs = new OpeningResponse();
            try
            {
                var rq = new OpeningRequest()
                {
                    Opening = new OpeningModel()
                    {
                        Id = Id
                    }
                };

                if (_openingService.DeleteOpening(rq).Successfull)
                {
                    return Ok();
                }

                throw new Exception("Could not delete the Opening!");
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
