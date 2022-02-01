using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface IClaimService
    {
        ClaimResponse GetClaims(ClaimRequest rq);
        ClaimResponse InsertUpdateClaim(ClaimRequest rq);
        OperationResponse DeleteClaim(ClaimRequest rq);
    }
}
