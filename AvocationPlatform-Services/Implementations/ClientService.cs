using AvocationPlatform_DatabaseAccess;
using AvocationPlatform_Models.DataModels;
using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using AvocationPlatform_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Implementations
{
    public class ClientService : BaseService, IClientService
    {
        #region Properties
        ClientManager _clientManager { get; set; }
        #endregion

        #region Constructor
        public ClientService() : base()
        {
            _clientManager = new ClientManager();
        }
        #endregion

        #region Public Methods

        public ClientResponse GetClients(ClientRequest rq)
        {
            return new ClientResponse()
            {
                Clients = _clientManager.GetClients(rq.Client.Id, rq.RecruiterId, rq.RoomId, rq.OpeningId, rq.CandidateId, rq.WithDeleted)
            };
        }

        public ClientResponse InsertUpdateClient(ClientRequest rq)
        {
            return new ClientResponse()
            {
                Clients = new List<ClientModel>
                {
                    _clientManager.InsertUpdateClient(rq.Client, rq.UserId)
                }
            };
        }

        public OperationResponse DeleteClient(ClientRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = _clientManager.DeleteClient(rq.Client.Id, rq.UserId)
            };
        }
        #endregion
    }
}
