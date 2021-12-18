using AvocationPlatform_Models.DataModels;
using AvocationPlatform_Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Responses
{
    public class OpeningResponse : ResponseBase
    {
        public IEnumerable<OpeningModel> Openings { get; set; }
    }
}
