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
    public class ClaimManager : ConfigBase
    {
        public IEnumerable<ClaimModel> GetClaims(Guid? pClaimId = null, Guid? pUserId = null, Guid? pRoleId = null,
            bool WithDeleted = false)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ClaimId", pClaimId, DbType.Guid);
            parameters.Add("@UserId", pUserId, DbType.Guid);
            parameters.Add("@RoleId", pRoleId, DbType.Guid);
            parameters.Add("@WithDeleted", WithDeleted, DbType.Boolean);

            var rs = SqlHelper.QuerySP<ClaimModel>("spGetClaims", parameters, null, null, false, 0, Connection.ConnectionString);

            if (rs != null)
                return rs.ToList();
            else
                return new List<ClaimModel>();
        }

        public ClaimModel InsertUpdateClaim(ClaimModel rq, string Username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ClaimId", rq.Id, DbType.Guid);
            parameters.Add("@Name", rq.Name, DbType.String);
            parameters.Add("@Value", rq.Value, DbType.String);
            parameters.Add("@UserId", rq.UserId, DbType.Guid);
            parameters.Add("@RoleId", rq.RoleId, DbType.Guid);
            parameters.Add("@AllowedOn", rq.AllowedOn, DbType.DateTime);
            parameters.Add("@ExpiresOn", rq.ExpiresOn, DbType.DateTime);
            parameters.Add("@Username", Username, DbType.String);

            var rs = SqlHelper.ExecuteScalar("spInsertUpdateClaim", parameters, null, null, 0, Connection.ConnectionString);

            if (rs != null)
                rq.Id = (Guid)rs;
            else
                rq.Id = null;

            return rq;
        }
        public bool DeleteClaim(Guid? pClaimId, string pUsername)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ClaimId", pClaimId, DbType.Guid);
            parameters.Add("@Username", pUsername, DbType.String);

            var rs = SqlHelper.ExecuteSP("spDeleteClaim", parameters, null, null, 0, Connection.ConnectionString);

            return rs >= 0;
        }
    }
}
