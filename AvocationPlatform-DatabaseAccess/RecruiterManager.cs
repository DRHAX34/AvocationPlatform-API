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
    public class RecruiterManager : ConfigBase
    {
        public IEnumerable<RecruiterModel> GetRecruiters(Guid? pRecruiterId = null,
            Guid? pCandidateId = null, Guid? pClientId = null, Guid? pRoomId = null, Guid? pOpeningId = null, bool WithDeleted = false)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RecruiterId", pRecruiterId, DbType.Guid);
            parameters.Add("@CandidateId", pCandidateId, DbType.Guid);
            parameters.Add("@ClientId", pClientId, DbType.Guid);
            parameters.Add("@OpeningId", pOpeningId, DbType.Guid);
            parameters.Add("@RoomId", pRoomId, DbType.Guid);
            parameters.Add("@WithDeleted", WithDeleted, DbType.Boolean);

            var rs = SqlHelper.QuerySP<RecruiterModel>("spGetRecruiters", parameters, null, null, false, 0, Connection.ConnectionString);

            if (rs != null)
                return rs.ToList();
            else
                return new List<RecruiterModel>();
        }

        public RecruiterModel InsertUpdateRecruiter(RecruiterModel rq, string Username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RecruiterId", rq.Id, DbType.Guid);
            parameters.Add("@Name", rq.Name, DbType.String);
            parameters.Add("@SysStatus", rq.SysStatus, DbType.String);
            parameters.Add("@Username", Username, DbType.String);

            var rs = SqlHelper.ExecuteScalar("spInsertUpdateRecruiter", parameters, null, null, 0, Connection.ConnectionString);

            if (rs != null)
                rq.Id = (Guid)rs;
            else
                rq.Id = null;

            return rq;
        }
        public bool DeleteRecruiter(Guid? pRecruiterId, string pUsername)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RecruiterId", pRecruiterId, DbType.Guid);
            parameters.Add("@Username", pUsername, DbType.String);

            var rs = SqlHelper.ExecuteSP("spDeleteRecruiter", parameters, null, null, 0, Connection.ConnectionString);

            return rs >= 0;
        }
    }
}
