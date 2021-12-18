using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class RoomRequest : RequestBase
    {
        public RoomModel Room { get; set; } = new RoomModel();
        public Guid? RecruiterId { get; set; } = null;
        public Guid? CandidateId { get; set; } = null;
        public Guid? ClientId { get; set; } = null;
        public Guid? OpeningId { get; set; } = null;

    }
}
