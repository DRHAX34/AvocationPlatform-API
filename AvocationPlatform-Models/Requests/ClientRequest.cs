using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class ClientRequest : RequestBase
    {
        public ClientModel Client { get; set; } = new ClientModel();
        public Guid? RecruiterId { get; set; } = null;
        public Guid? RoomId { get; set; } = null;
        public Guid? OpeningId { get; set; } = null;
        public Guid? CandidateId { get; set; } = null;
    }
}
