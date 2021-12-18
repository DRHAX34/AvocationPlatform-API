using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface IRoomService
    {
        RoomResponse GetRooms(RoomRequest rq);
        RoomResponse InsertUpdateRoom(RoomRequest rq);
        OperationResponse DeleteRoom(RoomRequest rq);
    }
}
