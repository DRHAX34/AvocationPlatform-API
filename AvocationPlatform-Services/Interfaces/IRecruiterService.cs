using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface IRecruiterService
    {
        RecruiterResponse GetRecruiters(RecruiterRequest rq);
        RecruiterResponse InsertUpdateRecruiter(RecruiterRequest rq);
        OperationResponse DeleteRecruiter(RecruiterRequest rq);
    }
}
