using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AvocationPlatform_Models.DataModels
{
    public class JwtTokenModel
    {

        public JwtTokenModel()
        {
            //Empty Token
        }

        public JwtTokenModel(string Header, string Body, string Signature)
        {
            this.Header = JsonSerializer.Deserialize<HeaderModel>(Convert.FromBase64String(Header));
            this.Body = JsonSerializer.Deserialize<BodyModel>(Convert.FromBase64String(Body));
            this.Signature = Signature;
        }

        public HeaderModel Header { get; set; }
        public BodyModel Body { get; set; }
        public string Signature { get; set; }

        public override string ToString()
        {
            var stringRepresentation = new StringBuilder();

            if (Header != null)
            {
                stringRepresentation.Append(Convert.ToBase64String(JsonSerializer.SerializeToUtf8Bytes(Header)));
            }

            if (Body != null)
            {
                if(stringRepresentation.Length > 0)
                    stringRepresentation.Append('.');

                stringRepresentation.Append(Convert.ToBase64String(JsonSerializer.SerializeToUtf8Bytes(Header)));
            } 

            if (string.IsNullOrWhiteSpace(Signature))
            {
                if (stringRepresentation.Length > 0)
                    stringRepresentation.Append('.');
                stringRepresentation.Append(Signature);
            }

            return stringRepresentation.ToString();
        }

        public class HeaderModel
        {
            private HeaderModel()
            {
                //Private Constructor
            }

            public string alg { get; set; }
            public string typ { get; set; }
        }

        public class BodyModel
        {
            private BodyModel()
            {
                //Private Constructor
            }

            public Guid? oid { get; set; }
            public string iss { get; set; }
            public string name { get; set; }
            public string preferred_username { get; set; }
            public string email { get; set; }
            public string ver { get; set; }
        }
    }

}
