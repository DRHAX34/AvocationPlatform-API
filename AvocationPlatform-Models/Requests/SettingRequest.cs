using AvocationPlatform_Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_Models.Requests
{
    public class SettingRequest : RequestBase
    {
        /// <summary>
        /// Contains the Setting
        /// </summary>
        public SettingsModel Setting { get; set; }
    }
}
