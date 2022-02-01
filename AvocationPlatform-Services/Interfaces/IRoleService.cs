using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface IRoleService
    {
        #region User Roles
        OperationResponse InsertUserRole(RoleRequest rq);
        OperationResponse DeleteUserRole(RoleRequest rq);
        #endregion

        #region Roles
        RoleResponse GetRoles(RoleRequest rq);
        RoleResponse InsertUpdateRole(RoleRequest rq);
        OperationResponse DeleteRole(RoleRequest rq);
        #endregion
    }
}
