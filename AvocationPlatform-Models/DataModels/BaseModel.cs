using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.DataModels
{
    public class BaseModel
    {
        public Guid? Id { get; set; }

        public string SysStatus { get; set; } = Constants.SysStatus.Enabled;
        public DateTime SysCreateDate { get; set; }
        public string SysCreateUserId { get; set; }
        public DateTime SysModifyDate { get; set; }
        public string SysModifyUserId { get; set; }
    }
}
