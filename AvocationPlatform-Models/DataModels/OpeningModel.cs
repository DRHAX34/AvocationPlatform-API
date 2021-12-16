using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.DataModels
{
    public class OpeningModel : BaseModel
    {
        public Guid? ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
