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
    public class ClaimService : BaseService, IClaimService
    {
        #region Properties
        ClaimManager claimManager { get; set; }
        #endregion

        #region Constructor
        public ClaimService() : base()
        {
            claimManager = new ClaimManager();
        }
        #endregion

        #region Methods

        public ClaimResponse GetClaims(ClaimRequest rq)
        {
            return new ClaimResponse()
            {
                Claims = claimManager.GetClaims(rq.Claim.Id, rq.Claim.UserId, rq.Claim.RoleId, rq.WithDeleted)
            };
        }

        public ClaimResponse InsertUpdateClaim(ClaimRequest rq)
        {
            return new ClaimResponse()
            {
                Claims = new List<ClaimModel>()
                {
                    claimManager.InsertUpdateClaim(rq.Claim, rq.Username)
                }
            };
        }
        public OperationResponse DeleteClaim(ClaimRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = claimManager.DeleteClaim(rq.Claim.Id, rq.Username)
            };
        }

        #endregion
    }
}
