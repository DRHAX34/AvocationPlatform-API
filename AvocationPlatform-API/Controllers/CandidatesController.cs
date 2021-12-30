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

                rs = _candidateService.GetCandidates(rq);

                //Clear the Picture Path from the server
                foreach (var client in rs.Candidates)
                {
                    if (string.IsNullOrWhiteSpace(client.ProfilePictureUri) || !System.IO.File.Exists(client.ProfilePictureUri))
                        client.ProfilePictureUri = null;
                    else
                        client.ProfilePictureUri = "HAS_IMAGE";
                }

                return Ok(rs);
            }
            catch (Exception ex)
            {
                rs.Error.Status = AvocationPlatform_Models.Enumerations.ErrorStatus.Error;
                rs.Error.Message = ex.Message;
                rs.Error.HttpStatus = System.Net.HttpStatusCode.BadRequest;

                return BadRequest(rs);
            }
        }

        [HttpGet("Picture")]
        public IActionResult GetCandidatePicture([FromQuery] Guid Id)
        {
            try
            {
                var candidate = _candidateService.GetCandidates(new CandidateRequest()
                {
                    Candidate = new CandidateModel()
                    {
                        Id = Id
                    }
                })?.Candidates?.FirstOrDefault();

                if (candidate == null || string.IsNullOrWhiteSpace(candidate.ProfilePictureUri) || !System.IO.File.Exists(candidate.ProfilePictureUri))
                {
                    return NotFound("The specified Candidate picture was not found!");
                }

                return PhysicalFile(candidate.ProfilePictureUri, MimeTypes.MimeTypeMap.GetMimeType(Path.GetExtension(candidate.ProfilePictureUri)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("Picture")]
        [Consumes("multipart/form-data")]
        [RequestFormLimits(MultipartBodyLengthLimit = 500000000)]
        public IActionResult SetCandidatePicture([FromQuery] Guid Id, [FromForm] IFormFile CandidatePicture)
        {
            var rs = new CandidateResponse();
            try
            {
                var candidate = _candidateService.GetCandidates(new CandidateRequest()
                {
                    Candidate = new CandidateModel()
                    {
                        Id = Id
                    }
                })?.Candidates?.FirstOrDefault();

                //TODO: Change this to an app setting
                var pictureFolder = Path.Combine("C:\\AvocationStorage", "AvocationPictures");

                Directory.CreateDirectory(pictureFolder);

                if (candidate == null)
                {
                    throw new ArgumentNullException("CandidateId", "CandidateId is not valid!");
                }

                if (!string.IsNullOrWhiteSpace(candidate.ProfilePictureUri))
                {
                    if (System.IO.File.Exists(candidate.ProfilePictureUri))
                    {
                        System.IO.File.Delete(candidate.ProfilePictureUri);
                    }
                }
                else
                {
                    candidate.ProfilePictureUri = Path.Combine(pictureFolder,
                        Path.ChangeExtension(Path.GetRandomFileName(),
                        MimeTypes.MimeTypeMap.GetExtension(CandidatePicture.ContentType)));
                }

                //Write the new picture
                using (var newPicture = new FileStream(candidate.ProfilePictureUri, FileMode.Create))
                {
                    CandidatePicture.CopyTo(newPicture);
                }

                rs = _candidateService.InsertUpdateCandidate(new CandidateRequest()
                {
                    Candidate = candidate,
                    UserId = "Teste" //TODO: Add authentication
                });

                if (rs?.Candidates?.FirstOrDefault().Id != null)
                    rs.Candidates.FirstOrDefault().ProfilePictureUri = "HAS_IMAGE";

                return Ok(rs);
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
                if (rq.Candidate.ProfilePictureUri == "HAS_IMAGE")
                {
                    var originalCandidate = _candidateService.GetCandidates(rq)?.Candidates?.FirstOrDefault();
                    if (originalCandidate != null)
                        rq.Candidate.ProfilePictureUri = originalCandidate.ProfilePictureUri;
                }

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
