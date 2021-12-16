using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class CandidateRequest : RequestBase
    {
        public CandidateModel Candidate { get; set; }
    }
}
