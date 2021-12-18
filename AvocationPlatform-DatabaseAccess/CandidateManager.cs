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
    public class CandidateManager : ConfigBase
    {
        public IEnumerable<CandidateModel> GetCandidates(Guid? pCandidateId = null,
            Guid? pRecruiterId = null, Guid? pRoomId = null, Guid? pOpeningId = null, Guid? pClientId = null, bool WithDeleted = false)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CandidateId", pCandidateId, DbType.Guid);
            parameters.Add("@OpeningId", pOpeningId, DbType.Guid);
            parameters.Add("@ClientId", pClientId, DbType.Guid);
            parameters.Add("@RecruiterId", pRecruiterId, DbType.Guid);
            parameters.Add("@RoomId", pRoomId, DbType.Guid);
            parameters.Add("@WithDeleted", WithDeleted, DbType.Boolean);

            var rs = SqlHelper.QuerySP<CandidateModel>("spGetCandidates", parameters, null, null, false, 0, Connection.ConnectionString);

            if (rs != null)
                return rs.ToList();
            else
                return new List<CandidateModel>();
        }

        public CandidateModel InsertUpdateCandidate(CandidateModel rq, string Username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CandidateId", rq.Id, DbType.Guid);
            parameters.Add("@Name", rq.Name, DbType.String);
            parameters.Add("@Surname", rq.Surname, DbType.String);
            parameters.Add("@PreferredName", rq.PreferredName, DbType.String);
            parameters.Add("@Email", rq.Email, DbType.String);
            parameters.Add("@ProfilePictureUri", rq.ProfilePictureUri, DbType.String);
            parameters.Add("@Phone", rq.Phone, DbType.String);
            parameters.Add("@VAT", rq.VAT, DbType.String);
            parameters.Add("@Company", rq.Company, DbType.String);
            parameters.Add("@Address", rq.Address, DbType.String);
            parameters.Add("@ZipCode", rq.ZipCode, DbType.String);
            parameters.Add("@City", rq.City, DbType.String);
            parameters.Add("@SysStatus", rq.SysStatus, DbType.String);
            parameters.Add("@Username", Username, DbType.String);

            var rs = SqlHelper.ExecuteScalar("spInsertUpdateCandidate", parameters, null, null, 0, Connection.ConnectionString);

            if (rs != null)
                rq.Id = (Guid)rs;
            else
                rq.Id = null;

            return rq;
        }
        public bool DeleteCandidate(Guid? pCandidateId, string pUsername)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CandidateId", pCandidateId, DbType.Guid);
            parameters.Add("@Username", pUsername, DbType.String);

            var rs = SqlHelper.ExecuteSP("spDeleteCandidate", parameters, null, null, 0, Connection.ConnectionString);

            return rs >= 0;
        }
    }
}
