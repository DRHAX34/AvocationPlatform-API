using AvocationPlatform_DatabaseAccess;
using AvocationPlatform_Models.DataModels;
using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using AvocationPlatform_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AvocationPlatform_Services.Implementations
{
    public class TokenService : BaseService, ITokenService
    {
        #region Properties
        TokenManager tokenManager { get; set; }
        #endregion

        #region Constructors
        public TokenService() : base()
        {
            tokenManager = new TokenManager();
        }
        #endregion

        #region Methods

        public TokenResponse GetTokenHistory(TokenRequest rq)
        {
            return new TokenResponse()
            {
                Tokens = tokenManager.GetTokenHistory(rq.Token.Id, rq.Token.RefreshToken, rq.UserId, rq.WithDeleted)
            };
        }

        public TokenResponse InsertUpdateTokenHistory(TokenRequest rq)
        {
            return new TokenResponse()
            {
                Tokens = new List<TokenModel>() {
                    tokenManager.InsertUpdateTokenHistory(rq.Token, rq.Username)
                }
            };
        }
        public OperationResponse DeleteTokenHistory(TokenRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = tokenManager.DeleteTokenHistory(rq.Token, rq.Username)
            };
        }

        public JwtTokenResponse SignToken(UserModel rq, string username)
        {
            var jwtToken = new JwtTokenModel()
            {
                Header =
                {
                    alg = "HS512",
                    typ = "JWT"
                },
                Body =
                {
                    oid = rq.Id,
                    name = String.Concat(rq.FirstName, " ", rq.LastName),
                    preferred_username = rq.Username,
                    email = rq.Email,
                    iss = "AvocationPlatform",
                    ver = "1.0"
                }
            };
            var token = new TokenModel();
            var refreshKey = string.Empty;

            // Create a random key using a random number generator. This would be the
            //  secret key shared by sender and receiver.
            byte[] secretkey = new byte[256];
            byte[] refreshkey = new byte[256];
            //RNGCryptoServiceProvider is an implementation of a random number generator.
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                // The array is now filled with cryptographically strong random bytes.
                rng.GetBytes(secretkey);

                // Generate a refresh key
                rng.GetBytes(refreshkey);

                // Convert key to base 64
                refreshKey = Convert.ToBase64String(refreshkey);
            }

            //Sign the token
            jwtToken = GetTokenSignature(jwtToken, secretkey);
            token.Token = jwtToken.ToString();
            token.RefreshToken = string.Concat(refreshKey, GetHmacSha512Signature(refreshKey, secretkey));
            token.SignatureKey = Convert.ToBase64String(secretkey);
            token.AllowedOn = DateTime.UtcNow;
            token.ExpiresOn = DateTime.UtcNow.AddMinutes(15);
            token.UserId = rq.Id;

            var tokenHistory = tokenManager.InsertUpdateTokenHistory(token, username);

            //Token has not been generated
            if (!tokenHistory.Id.HasValue)
                return null;

            return new JwtTokenResponse()
            {
                Token = jwtToken
            };
        }

        public OperationResponse ValidateToken(TokenRequest rq)
        {
            var tokenSignatureKey = tokenManager.ValidateToken(rq.Token.Token);
            if (string.IsNullOrWhiteSpace(tokenSignatureKey)){
                var tokenParts = rq.Token.Token.Split(new char[] { '.' });

                var newSignature = GetTokenSignature(new JwtTokenModel(tokenParts[0], tokenParts[1], string.Empty), Convert.FromBase64String(tokenSignatureKey));

                //Return the result of the validation
                return new OperationResponse()
                {
                    Successfull = tokenParts[2].Equals(newSignature.Signature)
                };
            }

            return new OperationResponse()
            {
                Successfull = false
            };
        }

        public OperationResponse InvalidateToken(TokenRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = tokenManager.InvalidateToken(rq.Token.Token, rq.Username)
            };
        }
        #endregion

        #region Private Methods
        private JwtTokenModel GetTokenSignature(JwtTokenModel tokenModel, byte[] key)
        {
            // Set the signature
            tokenModel.Signature = GetHmacSha512Signature(tokenModel.ToString(), key);
            return tokenModel;
        }

        private string GetHmacSha512Signature(string valueToSign, byte[] key)
        {
            // Initialize the keyed hash object.
            using (HMACSHA512 hmac = new HMACSHA512(key))
            {
                // Compute the hash of the input file.
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(valueToSign));

                // Set the signature
                return Convert.ToBase64String(hashValue);
            }
        }
        #endregion
    }
}
