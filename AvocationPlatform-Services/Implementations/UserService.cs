using AvocationPlatform_DatabaseAccess;
using AvocationPlatform_Models.DataModels;
using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using AvocationPlatform_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AvocationPlatform_Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        #region Properties
        UserManager userManager { get; set; }
        #endregion

        #region Constructor
        public UserService() : base()
        {
            userManager = new UserManager();
        }
        #endregion

        #region Methods
        public UserResponse GetUsers(UserRequest rq)
        {
            return new UserResponse()
            {
                Users = userManager.GetUsers(rq.User.Id, rq.RoleId)
            };
        }

        public UserResponse InsertUpdateUsers(UserRequest rq)
        {
            //Set email always to lower variant
            rq.User.Email = rq.User.Email.ToLower();

            //Check if this is an update password type of update
            if (string.IsNullOrWhiteSpace(rq.User.HashedPassword))
            {
                //Hash the password
                rq.User.HashedPassword = GetHmacSha512Signature(string.Concat(rq.User.Email, ":", rq.User.HashedPassword), Encoding.UTF8.GetBytes(rq.User.Email));
            }

            //Update the user
            return new UserResponse()
            {
                Users = new List<UserModel>()
                {
                    userManager.InsertUpdateUsers(rq.User, rq.Username)
                }
            };
        }

        public OperationResponse DeleteUsers(UserRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = userManager.DeleteUser(rq.User.Id, rq.Username)
            };
        }
        #endregion

        #region Private Methods
        private string GetHmacSha512Signature(string valueToSign, byte[] key)
        {
            // Initialize the keyed hash object.
            using (HMACSHA512 hmac = new HMACSHA512(key))
            {
                // Compute the hash of the input file.
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(valueToSign));

                // Set the signature
                return Convert.ToBase64String(hashValue);
            }
        }
        #endregion
    }
}
