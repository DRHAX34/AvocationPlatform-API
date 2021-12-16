using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.DataModels
{
    public class AppointmentModel : BaseModel
    {
        public Guid? CandidateId { get; set; }
        public Guid? RecruiterId { get; set; }
        public Guid? RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? OpeningId { get; set; }
        public int Stage { get; set; }
        public string SysSingleUserToken { get; set; }
    }
}
