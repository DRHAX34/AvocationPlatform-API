using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.DataModels
{
    public class TokenModel : BaseModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string SignatureKey { get; set; }
        public DateTime? AllowedOn { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public Guid? UserId { get; set; }
    }
}
