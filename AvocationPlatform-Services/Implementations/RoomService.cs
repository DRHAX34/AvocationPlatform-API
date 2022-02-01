using AvocationPlatform_DatabaseAccess;
using AvocationPlatform_Models.DataModels;
using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using AvocationPlatform_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Implementations
{
    public class RoomService : BaseService, IRoomService
    {
        #region Properties
        RoomManager _roomManager { get; set; }
        #endregion

        #region Constructor
        public RoomService() : base()
        {
            _roomManager = new RoomManager();
        }

        #endregion

        #region Public Methods
        public RoomResponse GetRooms(RoomRequest rq)
        {
            return new RoomResponse()
            {
                Rooms = _roomManager.GetRooms(rq.Room.Id, rq.RecruiterId, rq.CandidateId, rq.ClientId, rq.OpeningId, rq.WithDeleted)
            };
        }

        public RoomResponse InsertUpdateRoom(RoomRequest rq)
        {
            return new RoomResponse()
            {
                Rooms = new List<RoomModel>
                {
                    _roomManager.InsertUpdateRoom(rq.Room, rq.Username)
                }
            };
        }

        public OperationResponse DeleteRoom(RoomRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = _roomManager.DeleteRoom(rq.Room.Id, rq.Username)
            };
        }
        #endregion
    }
}
