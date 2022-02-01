using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface IUserService
    {
        UserResponse GetUsers(UserRequest rq);
        UserResponse InsertUpdateUsers(UserRequest rq);
        OperationResponse DeleteUsers(UserRequest rq);
    }
}
