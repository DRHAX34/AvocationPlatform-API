using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface ICandidateService
    {
        CandidateResponse GetCandidates(CandidateRequest rq);
        CandidateResponse InsertUpdateCandidate(CandidateRequest rq);
        OperationResponse DeleteCandidate(CandidateRequest rq);
    }
}
