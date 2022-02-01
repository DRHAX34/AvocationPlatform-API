using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class TokenRequest : RequestBase
    {
        public TokenModel Token { get; set; }
        public Guid? UserId { get; set; }
    }
}
