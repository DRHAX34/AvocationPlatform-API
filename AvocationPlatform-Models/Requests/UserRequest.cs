using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class UserRequest : RequestBase
    {
        /// <summary>
        /// Contains the user
        /// </summary>
        public UserModel User { get; set; }

        /// <summary>
        /// Request users that belong to a single role
        /// </summary>
        public Guid? RoleId { get; set; }
    }
}
