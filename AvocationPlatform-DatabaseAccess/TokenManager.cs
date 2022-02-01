using AvocationPlatform_DatabaseAccess.Helper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using AvocationPlatform_Models.DataModels;

namespace AvocationPlatform_DatabaseAccess
{
    public class TokenManager : ConfigBase
    {
        #region Validation Methods
        public string ValidateToken(string Token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Token", Token, DbType.String);

            var rs = SqlHelper.QuerySP<string>("spValidateToken", parameters, null, null, false, 0, Connection.ConnectionString);

            if (rs != null && rs.Any())
                return rs.First();
            else
                return null;
        }
        public bool InvalidateToken(string Token, string Username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Token", Token, DbType.String);
            parameters.Add("@Username", Username, DbType.String);

            var rs = SqlHelper.QuerySP<string>("spInvalidateToken", parameters, null, null, false, 0, Connection.ConnectionString);

            return rs != null && rs.Any();
        }
        #endregion

        #region Token History Methods
        public IEnumerable<TokenModel> GetTokenHistory(Guid? TokenId = null, string RefreshToken = null, Guid? UserId = null, bool WithDeleted = false)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TokenId", TokenId, DbType.Guid);
            parameters.Add("@RefreshToken", RefreshToken, DbType.String);
            parameters.Add("@UserId", UserId, DbType.Guid);
            parameters.Add("@WithDeleted", WithDeleted, DbType.Boolean);

            var rs = SqlHelper.QuerySP<TokenModel>("spGetTokenHistory", parameters, null, null, false, 0, Connection.ConnectionString);

            if (rs != null)
                return rs.ToList();
            else
                return new List<TokenModel>();
        }

        public TokenModel InsertUpdateTokenHistory(TokenModel rq, string Username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TokenId", rq.Id, DbType.Guid);
            parameters.Add("@Token", rq.Token, DbType.String);
            parameters.Add("@RefreshToken", rq.RefreshToken, DbType.String);
            parameters.Add("@AllowedOn", rq.AllowedOn, DbType.DateTime);
            parameters.Add("@ExpiresOn", rq.ExpiresOn, DbType.DateTime);
            parameters.Add("@SysStatus", rq.SysStatus, DbType.String);
            parameters.Add("@UserId", rq.UserId, DbType.Guid);
            parameters.Add("@Username", Username, DbType.String);

            var rs = SqlHelper.ExecuteScalar("spInsertUpdateTokenHistory", parameters, null, null, 0, Connection.ConnectionString);

            if (rs != null)
                rq.Id = (Guid)rs;
            else
                rq.Id = null;

            return rq;
        }

        public bool DeleteTokenHistory(TokenModel rq, string Username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TokenId", rq.Id, DbType.Guid);
            parameters.Add("@Username", Username, DbType.String);

            var rs = SqlHelper.ExecuteSP("spDeleteTokenHistory", parameters, null, null, 0, Connection.ConnectionString);

            return rs >= 0;
        }
        #endregion
    }
}
