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
    public class AppointmentsController : ControllerBase
    {
        #region Properties
        IAppointmentService _appointmentService;
        #endregion

        #region Constructors
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        #endregion

        #region Public Actions
        [HttpGet("Get")]
        public IActionResult Get([FromQuery]Guid? pAppointmentId = null, [FromQuery] Guid? pCandidateId = null,
            [FromQuery] Guid? pRecruiterId = null, [FromQuery] Guid? pRoomId = null, [FromQuery] Guid? pOpeningId = null,
            [FromQuery] Guid? pClientId = null, [FromQuery] bool WithDeleted = false)
        {
            var rs = new AppointmentResponse();
            try
            {
                var rq = new AppointmentRequest()
                {
                    Appointment = new AppointmentModel()
                    {
                        Id = pAppointmentId,
                        CandidateId = pCandidateId,
                        RecruiterId = pRecruiterId,
                        RoomId = pRoomId,
                        OpeningId = pOpeningId,
                        ClientId = pClientId
                    },
                    WithDeleted = WithDeleted
                };

                return Ok(_appointmentService.GetAppointments(rq));
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
        public IActionResult InsertUpdate([FromBody] AppointmentRequest rq)
        {
            var rs = new AppointmentResponse();
            try
            {
                return Ok(_appointmentService.InsertUpdateAppointment(rq));
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
            var rs = new AppointmentResponse();
            try
            {
                var rq = new AppointmentRequest()
                {
                    Appointment = new AppointmentModel()
                    {
                        Id = Id
                    }
                };

                if (_appointmentService.DeleteAppointment(rq).Successfull)
                {
                    return Ok();
                }

                throw new Exception("Could not delete the Appointment!");
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
