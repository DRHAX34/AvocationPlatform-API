using AvocationPlatform_Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class RequestBase
    {
        public string UserId { get; set; }
        public Channel Channel { get; set; }
    }
}
