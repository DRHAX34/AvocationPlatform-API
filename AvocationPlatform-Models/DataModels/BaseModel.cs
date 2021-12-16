using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.DataModels
{
    public class BaseModel
    {
        public Guid? Id { get; set; }

        public string SysStatus { get; set; }
        public DateTime SysCreateDate { get; set; }
        public string SysCreateUser { get; set; }
        public DateTime SysModifyDate { get; set; }
        public string SysModifyUser { get; set; }
    }
}
