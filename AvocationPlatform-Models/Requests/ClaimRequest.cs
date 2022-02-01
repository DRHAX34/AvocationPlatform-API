using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class ClaimRequest : RequestBase
    {
        public ClaimModel Claim { get; set; }
    }
}
