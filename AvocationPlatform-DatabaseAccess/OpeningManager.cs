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
    public class OpeningManager : ConfigBase
    {
        public IEnumerable<OpeningModel> GetOpenings(Guid? pOpeningId = null,
            Guid? pRecruiterId = null, Guid? pClientId = null, Guid? pCandidateId = null, bool WithDeleted = false)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OpeningId", pOpeningId, DbType.Guid);
            parameters.Add("@ClientId", pClientId, DbType.Guid);
            parameters.Add("@RecruiterId", pRecruiterId, DbType.Guid);
            parameters.Add("@CandidateId", pCandidateId, DbType.Guid);
            parameters.Add("@WithDeleted", WithDeleted, DbType.Boolean);

            var rs = SqlHelper.QuerySP<OpeningModel>("spGetOpenings", parameters, null, null, false, 0, Connection.ConnectionString);

            if (rs != null)
                return rs.ToList();
            else
                return new List<OpeningModel>();
        }

        public OpeningModel InsertUpdateOpening(OpeningModel rq, string Username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OpeningId", rq.Id, DbType.Guid);
            parameters.Add("@ClientId", rq.ClientId, DbType.Guid);
            parameters.Add("@Title", rq.Title, DbType.String);
            parameters.Add("@Description", rq.Description, DbType.String);
            parameters.Add("@SysStatus", rq.SysStatus, DbType.String);
            parameters.Add("@Username", Username, DbType.String);

            var rs = SqlHelper.ExecuteScalar("spInsertUpdateOpening", parameters, null, null, 0, Connection.ConnectionString);

            if (rs != null)
                rq.Id = (Guid)rs;
            else
                rq.Id = null;

            return rq;
        }
        public bool DeleteOpening(Guid? pOpeningId, string pUsername)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OpeningId", pOpeningId, DbType.Guid);
            parameters.Add("@Username", pUsername, DbType.String);

            var rs = SqlHelper.ExecuteSP("spDeleteOpening", parameters, null, null, 0, Connection.ConnectionString);

            return rs >= 0;
        }
    }
}
