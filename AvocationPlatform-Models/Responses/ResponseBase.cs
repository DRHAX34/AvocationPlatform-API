using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.ResponseModels
{
    public class ResponseBase
    {
        public ErrorModel Error { get; set; } = new ErrorModel();
    }
}
