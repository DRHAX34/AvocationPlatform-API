using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface IAppointmentService
    {
        AppointmentResponse GetAppointments(AppointmentRequest rq);

        AppointmentResponse InsertUpdateAppointment(AppointmentRequest rq);

        OperationResponse DeleteAppointment(AppointmentRequest rq);
    }
}
