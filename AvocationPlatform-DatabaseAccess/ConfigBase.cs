using AvocationPlatform_DatabaseAccess.Config;
using System;
using System.Data.SqlClient;

namespace AvocationPlatform_DatabaseAccess
{
    public abstract class ConfigBase
    {
        protected SqlConnection Connection {
            get
            {
                return new SqlConnection(DataAccessSettings.ConnectionString);
            }
        }
    }
}
