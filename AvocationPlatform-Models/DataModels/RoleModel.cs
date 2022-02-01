using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.DataModels
{
    public class RoleModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Contains the users of this roles
        /// </summary>
        public IEnumerable<UserModel> Users { get; set; }

        /// <summary>
        /// Contains the claims of this role
        /// </summary>
        public IEnumerable<ClaimModel> Claims { get; set; }
    }
}
