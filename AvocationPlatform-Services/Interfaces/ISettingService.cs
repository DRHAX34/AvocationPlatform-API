using AvocationPlatform_Models.Requests;
using AvocationPlatform_Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Services.Interfaces
{
    public interface ISettingService
    {
        SettingResponse GetSettings(SettingRequest rq);
        SettingResponse InsertUpdateSetting(SettingRequest rq);
        OperationResponse DeleteSetting(SettingRequest rq);
    }
}
