using AvocationPlatform_DatabaseAccess.Helper;
using AvocationPlatform_Models.DataModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AvocationPlatform_DatabaseAccess
{
    public class UserManager : ConfigBase
    {
        public IEnumerable<UserModel> GetUsers(Guid? pUserId = null, Guid? pRoleId = null,
            bool WithDeleted = false)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", pUserId, DbType.Guid);
            parameters.Add("@RoleId", pRoleId, DbType.Guid);
            parameters.Add("@WithDeleted", WithDeleted, DbType.Boolean);

            var rs = SqlHelper.QuerySP<UserModel>("spGetUsers", parameters, null, null, false, 0, Connection.ConnectionString);

            if (rs != null)
                return rs.ToList();
            else
                return new List<UserModel>();
        }

        public UserModel InsertUpdateUsers(UserModel rq, string Username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", rq.Id, DbType.Guid);
            parameters.Add("@Photo", rq.Photo, DbType.String);
            parameters.Add("@FirstName", rq.FirstName, DbType.String);
            parameters.Add("@LastName", rq.LastName, DbType.String);
            parameters.Add("@AffectedUsername", rq.Username, DbType.String);
            parameters.Add("@Email", rq.Email, DbType.String);
            parameters.Add("@Phone", rq.Phone, DbType.String);
            parameters.Add("@HashedPassword", rq.HashedPassword, DbType.String);
            parameters.Add("@SysStatus", rq.SysStatus, DbType.String);
            parameters.Add("@Username", Username, DbType.String);

            var rs = SqlHelper.ExecuteScalar("spInsertUpdateUser", parameters, null, null, 0, Connection.ConnectionString);

            if (rs != null)
                rq.Id = (Guid)rs;
            else
                rq.Id = null;

            return rq;
        }
        public bool DeleteUser(Guid? pUserId, string pUsername)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", pUserId, DbType.Guid);
            parameters.Add("@Username", pUsername, DbType.String);

            var rs = SqlHelper.ExecuteSP("spDeleteUser", parameters, null, null, 0, Connection.ConnectionString);

            return rs >= 0;
        }
    }
}
