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
    public class RoleService : BaseService, IRoleService
    {
        #region Properties
        RoleManager roleManager { get; set; }
        #endregion

        #region Constructor
        public RoleService() : base()
        {
            roleManager = new RoleManager();
        }
        #endregion

        public OperationResponse InsertUserRole(RoleRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = roleManager.InsertUserRole(rq.Role.Id.Value, rq.UserId.Value, rq.Username)
            };
        }

        public OperationResponse DeleteUserRole(RoleRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = roleManager.DeleteUserRole(rq.Role.Id.Value, rq.UserId.Value, rq.Username)
            };
        }

        public RoleResponse GetRoles(RoleRequest rq)
        {
            return new RoleResponse()
            {
                Roles = roleManager.GetRoles(rq.Role.Id.Value, rq.UserId.Value, rq.WithDeleted)
            };
        }

        public RoleResponse InsertUpdateRole(RoleRequest rq)
        {
            return new RoleResponse()
            {
                Roles = new List<RoleModel>()
                {
                    roleManager.InsertUpdateRole(rq.Role, rq.Username)
                }
            };
        }
        public OperationResponse DeleteRole(RoleRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = roleManager.DeleteRole(rq.Role.Id.Value, rq.Username)
            };
        }
    }
}
