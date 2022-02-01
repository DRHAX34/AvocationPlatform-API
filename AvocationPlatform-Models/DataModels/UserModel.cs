using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.DataModels
{
    public class UserModel : BaseModel
    {
        public Guid? Id { get; set; }
        public string Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HashedPassword { get; set; }
        
        /// <summary>
        /// Roles of the User
        /// </summary>
        public IEnumerable<RoleModel> Roles { get; set; }

        /// <summary>
        /// Claims of the User and associated Roles
        /// </summary>
        public IEnumerable<ClaimModel> Claims { get; set; }

    }
}
