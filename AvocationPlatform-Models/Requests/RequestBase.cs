using AvocationPlatform_Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class RequestBase
    {
        public string Username { get; set; }
        public Channel Channel { get; set; }
        public bool WithDeleted { get; set; } = false;
    }
}
