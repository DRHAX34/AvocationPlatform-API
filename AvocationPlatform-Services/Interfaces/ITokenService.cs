using AvocationPlatform_Models.DataModels;
using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface ITokenService
    {
        JwtTokenResponse SignToken(UserModel rq, string username);
        OperationResponse ValidateToken(TokenRequest rq);
        OperationResponse InvalidateToken(TokenRequest rq);
        TokenResponse GetTokenHistory(TokenRequest rq);
        TokenResponse InsertUpdateTokenHistory(TokenRequest rq);
        OperationResponse DeleteTokenHistory(TokenRequest rq);
    }
}
