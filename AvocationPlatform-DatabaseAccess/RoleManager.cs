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
    public class RoleManager : ConfigBase
    {
        #region User Roles

        public bool InsertUserRole(Guid pRoleId, Guid pUserId, string pUsername)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", pRoleId, DbType.Guid);
            parameters.Add("@UserId", pUserId, DbType.Guid);
            parameters.Add("@Username", pUsername, DbType.String);

            var rs = SqlHelper.ExecuteScalar("spInsertUserRole", parameters, null, null, 0, Connection.ConnectionString);

            return rs != null;
        }

        public bool DeleteUserRole(Guid pRoleId, Guid pUserId, string pUsername)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", pRoleId, DbType.Guid);
            parameters.Add("@UserId", pUserId, DbType.Guid);
            parameters.Add("@Username", pUsername, DbType.String);

            var rs = SqlHelper.ExecuteScalar("spDeleteUserRole", parameters, null, null, 0, Connection.ConnectionString);

            return rs != null;
        }

        #endregion

        public IEnumerable<RoleModel> GetRoles(Guid? pRoleId = null, Guid? pUserId = null,
            bool WithDeleted = false)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", pRoleId, DbType.Guid);
            parameters.Add("@UserId", pUserId, DbType.Guid);
            parameters.Add("@WithDeleted", WithDeleted, DbType.Boolean);

            var rs = SqlHelper.QuerySP<RoleModel>("spGetRoles", parameters, null, null, false, 0, Connection.ConnectionString);

            if (rs != null)
                return rs.ToList();
            else
                return new List<RoleModel>();
        }

        public RoleModel InsertUpdateRole(RoleModel rq, string Username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", rq.Id, DbType.Guid);
            parameters.Add("@Name", rq.Name, DbType.String);
            parameters.Add("@Description", rq.Description, DbType.String);
            parameters.Add("@SysStatus", rq.SysStatus, DbType.String);
            parameters.Add("@Username", Username, DbType.String);

            var rs = SqlHelper.ExecuteScalar("spInsertUpdateRole", parameters, null, null, 0, Connection.ConnectionString);

            if (rs != null)
                rq.Id = (Guid)rs;
            else
                rq.Id = null;

            return rq;
        }
        public bool DeleteRole(Guid? pRoleId, string pUsername)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", pRoleId, DbType.Guid);
            parameters.Add("@Username", pUsername, DbType.String);

            var rs = SqlHelper.ExecuteSP("spDeleteRole", parameters, null, null, 0, Connection.ConnectionString);

            return rs >= 0;
        }
    }
}
