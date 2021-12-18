using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class OpeningRequest : RequestBase
    {
        public OpeningModel Opening { get; set; } = new OpeningModel();
        public Guid? RecruiterId { get; set; } = null;
        public Guid? ClientId { get; set; } = null;
        public Guid? CandidateId { get; set; } = null;
    }
}
