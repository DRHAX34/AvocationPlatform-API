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
    public class OpeningService : BaseService, IOpeningService
    {
        #region Properties
        OpeningManager _openingManager { get; set; }
        #endregion

        #region Constructor
        public OpeningService() : base()
        {
            _openingManager = new OpeningManager();
        }
        #endregion

        #region Public Methods

        public OpeningResponse GetOpenings(OpeningRequest rq)
        {
            return new OpeningResponse()
            {
                Openings = _openingManager.GetOpenings(rq.Opening.Id, rq.RecruiterId, rq.ClientId, rq.CandidateId, rq.WithDeleted)
            };
        }

        public OpeningResponse InsertUpdateOpening(OpeningRequest rq)
        {
            return new OpeningResponse()
            {
                Openings = new List<OpeningModel>()
                {
                    _openingManager.InsertUpdateOpening(rq.Opening, rq.UserId)
                }
            };
        }
        public OperationResponse DeleteOpening(OpeningRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = _openingManager.DeleteOpening(rq.Opening.Id, rq.UserId)
            };
        }
        #endregion
    }
}
