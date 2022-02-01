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
    public class RecruiterService : BaseService, IRecruiterService
    {
        #region Properties
        RecruiterManager _recruiterManager { get; set; }
        #endregion

        #region Constructor
        public RecruiterService() : base()
        {
            _recruiterManager = new RecruiterManager();
        }
        #endregion

        #region Public Methods

        public RecruiterResponse GetRecruiters(RecruiterRequest rq)
        {
            return new RecruiterResponse()
            {
                Recruiters = _recruiterManager.GetRecruiters(rq.Recruiter.Id, rq.CandidateId, rq.ClientId, rq.RoomId, rq.OpeningId, rq.WithDeleted)
            };
        }

        public RecruiterResponse InsertUpdateRecruiter(RecruiterRequest rq)
        {
            return new RecruiterResponse()
            {
                Recruiters = new List<RecruiterModel>()
                {
                    _recruiterManager.InsertUpdateRecruiter(rq.Recruiter, rq.Username)
                }
            };
        }
        public OperationResponse DeleteRecruiter(RecruiterRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = _recruiterManager.DeleteRecruiter(rq.Recruiter.Id, rq.Username)
            };
        }
        #endregion
    }
}
