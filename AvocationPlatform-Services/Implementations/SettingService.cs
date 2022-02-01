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
    public class SettingService : BaseService, ISettingService
    {
        #region Properties
        SettingsManager settingsManager { get; set; }
        #endregion

        #region Constructor
        public SettingService() : base()
        {
            settingsManager = new SettingsManager();
        }

        #endregion

        #region Methods
        public SettingResponse GetSettings(SettingRequest rq)
        {
            return new SettingResponse()
            {
                Settings = settingsManager.GetSettings(rq.Setting.Id, rq.Setting.UserId, rq.Setting.RoleId, rq.WithDeleted)
            };
        }

        public SettingResponse InsertUpdateSetting(SettingRequest rq)
        {
            return new SettingResponse()
            {
                Settings = new List<SettingsModel>()
                {
                    settingsManager.InsertUpdateSetting(rq.Setting, rq.Username)
                }
            };
        }

        public OperationResponse DeleteSetting(SettingRequest rq)
        {
            return new OperationResponse()
            {
                Successfull = settingsManager.DeleteSetting(rq.Setting.Id, rq.Username)
            };
        }
        #endregion
    }
}
