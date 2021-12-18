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
    public class ClientsController : ControllerBase
    {
        #region Properties
        IClientService _clientService;
        #endregion

        #region Constructors
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }
        #endregion

        #region Public Actions
        [HttpGet("Get")]
        public IActionResult Get([FromQuery]Guid? pClientId = null,
            [FromQuery] Guid? pRecruiterId = null, [FromQuery] Guid? pRoomId = null,
            [FromQuery] Guid? pOpeningId = null, [FromQuery] Guid? pCandidateId = null,
            [FromQuery] bool WithDeleted = false)
        {
            var rs = new ClientResponse();
            try
            {
                var rq = new ClientRequest()
                {
                    Client = new ClientModel()
                    {
                        Id = pClientId
                    },
                    CandidateId = pCandidateId,
                    RecruiterId = pRecruiterId,
                    RoomId = pRoomId,
                    OpeningId = pOpeningId,
                    WithDeleted = WithDeleted
                };

                rs = _clientService.GetClients(rq);

                //Clear the Picture Path from the server
                foreach(var client in rs.Clients)
                {
                    if (string.IsNullOrWhiteSpace(client.PictureUri) || !System.IO.File.Exists(client.PictureUri))
                        client.PictureUri = null;
                    else
                        client.PictureUri = "HAS_IMAGE";
                }

                return Ok(rs);
            } catch(Exception ex)
            {
                rs.Error.Status = AvocationPlatform_Models.Enumerations.ErrorStatus.Error;
                rs.Error.Message = ex.Message;
                rs.Error.HttpStatus = System.Net.HttpStatusCode.BadRequest;

                return BadRequest(rs);
            }
        }

        [HttpGet("Picture")]
        public IActionResult GetClientPicture([FromQuery]Guid ClientId)
        {
            try
            {
                var client = _clientService.GetClients(new ClientRequest()
                {
                    Client = new ClientModel()
                    {
                        Id = ClientId
                    }
                })?.Clients?.FirstOrDefault();

                if(client == null || string.IsNullOrWhiteSpace(client.PictureUri) || !System.IO.File.Exists(client.PictureUri))
                {
                    return NotFound("The specified Client picture was not found!");
                }

                return PhysicalFile(client.PictureUri, MimeTypes.MimeTypeMap.GetMimeType(Path.GetExtension(client.PictureUri)));
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("Picture")]
        [Consumes("multipart/form-data")]
        [RequestFormLimits(MultipartBodyLengthLimit = 500000000)]
        public IActionResult SetClientPicture([FromQuery]Guid ClientId, [FromForm]IFormFile ClientPicture)
        {
            var rs = new ClientResponse();
            try
            {
                var client = _clientService.GetClients(new ClientRequest()
                {
                    Client = new ClientModel()
                    {
                        Id = ClientId
                    }
                })?.Clients?.FirstOrDefault();

                //TODO: Change this to an app setting
                var pictureFolder = Path.Combine(Path.GetTempPath(), "/AvocationPictures");
                
                if(client == null)
                {
                    throw new ArgumentNullException("ClientId", "ClientId is not valid!");
                }

                if(!string.IsNullOrWhiteSpace(client.PictureUri))
                {
                    if (System.IO.File.Exists(client.PictureUri))
                    {
                        System.IO.File.Delete(client.PictureUri);
                    }
                } else
                {
                    client.PictureUri = Path.Combine(pictureFolder, 
                        Path.ChangeExtension(Path.GetRandomFileName(), 
                        MimeTypes.MimeTypeMap.GetExtension(ClientPicture.ContentType)));
                }

                //Write the new picture
                using(var newPicture = new FileStream(client.PictureUri, FileMode.Create))
                {
                    ClientPicture.CopyTo(newPicture);
                }

                rs = _clientService.InsertUpdateClient(new ClientRequest()
                {
                    Client = client
                });

                if(rs?.Clients?.FirstOrDefault() != null)
                    rs.Clients.FirstOrDefault().PictureUri = "HAS_IMAGE";

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
        public IActionResult InsertUpdate([FromBody] ClientRequest rq)
        {
            var rs = new ClientResponse();
            try
            {
                if (rq.Client.PictureUri == "HAS_IMAGE")
                {
                    var originalClient = _clientService.GetClients(rq)?.Clients?.FirstOrDefault();
                    if (originalClient != null)
                        rq.Client.PictureUri = originalClient.PictureUri;
                }

                return Ok(_clientService.InsertUpdateClient(rq));
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
        public IActionResult Delete([FromQuery]Guid Id)
        {
            var rs = new ClientResponse();
            try
            {
                var rq = new ClientRequest()
                {
                    Client = new AvocationPlatform_Models.DataModels.ClientModel()
                    {
                        Id = Id
                    }
                };

                if (_clientService.DeleteClient(rq).Successfull)
                {
                    return Ok();
                }

                throw new Exception("Could not delete the Client!");
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
