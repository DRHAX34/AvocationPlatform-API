using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class CandidateRequest : RequestBase
    {
        public CandidateModel Candidate { get; set; } = new CandidateModel();
        public Guid? RecruiterId { get; set; } = null;
        public Guid? RoomId { get; set; } = null;
        public Guid? OpeningId { get; set; } = null;
        public Guid? ClientId { get; set; } = null;
    }
}
