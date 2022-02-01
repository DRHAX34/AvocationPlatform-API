using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.DataModels
{
    public class SettingsModel : BaseModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid? UserId { get; set; }
        public Guid? RoleId { get; set; }
    }
}
