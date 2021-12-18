using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface IOpeningService
    {
        OpeningResponse GetOpenings(OpeningRequest rq);
        OpeningResponse InsertUpdateOpening(OpeningRequest rq);
        OperationResponse DeleteOpening(OpeningRequest rq);
    }
}
