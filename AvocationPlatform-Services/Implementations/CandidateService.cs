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
    public class CandidateService : BaseService, ICandidateService
    {
        #region Properties
        CandidateManager _candidateManager { get; set; }
        #endregion

        #region Constructors
        public CandidateService() : base()
        {
            _candidateManager = new CandidateManager();
        }
        #endregion

        #region Public Methods
        public CandidateResponse GetCandidates(CandidateRequest rq)
        {
            return new CandidateResponse()
            {
                Candidates = _candidateManager.GetCandidates(rq.Candidate.Id, rq.RecruiterId, rq.RoomId, rq.OpeningId, rq.ClientId, rq.WithDeleted)
            };
        }

        public CandidateResponse InsertUpdateCandidate(CandidateRequest rq)
        {
            return new CandidateResponse()
            {
                Candidates = new List<CandidateModel>()
                {
                    _candidateManager.InsertUpdateCandidate(rq.Candidate, rq.UserId)
                }
            };
        }

        public OperationResponse DeleteCandidate(CandidateRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = _candidateManager.DeleteCandidate(rq.Candidate.Id, rq.UserId)
            };
        }
        #endregion

    }
}
