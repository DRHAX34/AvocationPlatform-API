using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class RecruiterRequest : RequestBase
    {
        public RecruiterModel Recruiter { get; set; } = new RecruiterModel();
        public Guid? CandidateId { get; set; } = null;
        public Guid? ClientId { get; set; } = null;
        public Guid? RoomId { get; set; } = null;
        public Guid? OpeningId { get; set; } = null;
    }
}
