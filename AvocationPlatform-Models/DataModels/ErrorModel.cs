using AvocationPlatform_Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AvocationPlatform_Models.DataModels
{
    public class ErrorModel
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public ErrorStatus Status { get; set; } = ErrorStatus.Success;
        public HttpStatusCode HttpStatus { get; set; }
    }
}
