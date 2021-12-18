using AvocationPlatform_DatabaseAccess.Helper;
using AvocationPlatform_Models.DataModels;
using AvocationPlatform_Models.Responses;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AvocationPlatform_DatabaseAccess
{
    public class AppointmentManager : ConfigBase
    {
        public IEnumerable<AppointmentModel> GetAppointments(Guid? pAppointmentId = null, Guid? pCandidateId = null,
            Guid? pRecruiterId = null, Guid? pRoomId = null, Guid? pOpeningId = null, Guid? pClientId = null, bool WithDeleted = false)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AppointmentId", pAppointmentId, DbType.Guid);
            parameters.Add("@CandidateId", pCandidateId, DbType.Guid);
            parameters.Add("@RecruiterId", pRecruiterId, DbType.Guid);
            parameters.Add("@RoomId", pRoomId, DbType.Guid);
            parameters.Add("@OpeningId", pOpeningId, DbType.Guid);
            parameters.Add("@ClientId", pClientId, DbType.Guid);
            parameters.Add("@WithDeleted", WithDeleted, DbType.Boolean);

            var rs = SqlHelper.QuerySP<AppointmentModel>("spGetAppointments", parameters, null, null, false, 0, Connection.ConnectionString);

            if (rs != null)
                return rs.ToList();
            else
                return new List<AppointmentModel>();
        }

        public AppointmentModel InsertUpdateAppointment(AppointmentModel rq, string Username)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AppointmentId", rq.Id, DbType.Guid);
            parameters.Add("@CandidateId", rq.CandidateId, DbType.Guid);
            parameters.Add("@RecruiterId", rq.RecruiterId, DbType.Guid);
            parameters.Add("@RoomId", rq.RoomId, DbType.Guid);
            parameters.Add("@StartTime", rq.StartTime, DbType.DateTime);
            parameters.Add("@EndTime", rq.EndTime, DbType.DateTime);
            parameters.Add("@OpeningId", rq.OpeningId, DbType.Guid);
            parameters.Add("@Stage", rq.Stage, DbType.Int32);
            parameters.Add("@SysStatus", rq.SysStatus, DbType.String);
            parameters.Add("@Username", Username, DbType.String);

            var rs = SqlHelper.ExecuteScalar("spInsertUpdateAppointment", parameters, null, null, 0, Connection.ConnectionString);

            if (rs != null)
                rq.Id = (Guid)rs;
            else
                rq.Id = null;

            return rq;
        }
        public bool DeleteAppointment(Guid? pAppointmentId, string pUsername)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AppointmentId", pAppointmentId, DbType.Guid);
            parameters.Add("@Username", pUsername, DbType.String);

            var rs = SqlHelper.ExecuteSP("spDeleteAppointment", parameters, null, null, 0, Connection.ConnectionString);

            return rs >= 0;
        }
    }
}
