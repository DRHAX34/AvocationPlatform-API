using AvocationPlatform_DatabaseAccess;
using AvocationPlatform_Models.DataModels;
using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using AvocationPlatform_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Implementations
{
    public class AppointmentService : BaseService, IAppointmentService
    {
        #region Properties
        AppointmentManager _appointmentsManager { get; set; }
        #endregion

        #region Constructors
        public AppointmentService() : base()
        {
            _appointmentsManager = new AppointmentManager();
        }
        #endregion

        #region Public Methods
        public AppointmentResponse GetAppointments(AppointmentRequest rq)
        {
            return new AppointmentResponse()
            {
                Appointments = _appointmentsManager.GetAppointments(rq.Appointment.Id, rq.Appointment.CandidateId, rq.Appointment.RecruiterId,
                    rq.Appointment.RoomId, rq.Appointment.OpeningId, rq.Appointment.ClientId, rq.WithDeleted)
            };
        }

        public AppointmentResponse InsertUpdateAppointment(AppointmentRequest rq)
        {
            return new AppointmentResponse()
            {
                Appointments = new List<AppointmentModel>
                {
                    _appointmentsManager.InsertUpdateAppointment(rq.Appointment, rq.Username)
                }
            };
        }

        public OperationResponse DeleteAppointment(AppointmentRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = _appointmentsManager.DeleteAppointment(rq.Appointment.Id, rq.Username)
            };
        }
        #endregion



    }
}
