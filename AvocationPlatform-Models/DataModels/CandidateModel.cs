using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.DataModels
{
    public class CandidateModel : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PreferredName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfilePictureUri { get; set; }
        public string VAT { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
