using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface IClientService
    {
        ClientResponse GetClients(ClientRequest rq);
        ClientResponse InsertUpdateClient(ClientRequest rq);
        OperationResponse DeleteClient(ClientRequest rq);
    }
}
