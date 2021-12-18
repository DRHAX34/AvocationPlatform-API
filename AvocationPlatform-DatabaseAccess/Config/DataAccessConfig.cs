using System;
using System.Collections.Generic;
using System.Text;

namespace AvocationPlatform_DatabaseAccess.Config
{
    public static class DataAccessConfig
    {
        public static void Set(string connectionString)
        {
            DataAccessSettings.ConnectionString = connectionString;
        }
    }
}
