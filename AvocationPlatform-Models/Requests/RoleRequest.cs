using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class RoleRequest : RequestBase
    {
        /// <summary>
        /// Contains the Role
        /// </summary>
        public RoleModel Role { get; set; }
        /// <summary>
        /// Gets the roles associated with this user
        /// </summary>
        public Guid? UserId { get; set; }
    }
}
