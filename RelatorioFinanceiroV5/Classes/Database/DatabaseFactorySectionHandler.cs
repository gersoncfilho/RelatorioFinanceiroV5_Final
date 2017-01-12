using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RelatorioFinanceiroV5.Classes.Database
{
    public sealed class DatabaseFactorySectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("Name")]
        public string Name
        {

            get { return (string)base["Name"]; }

        }

        [ConfigurationProperty("ConnectionStringName")]
        public string ConnectionStringName
        {

            get { return (string)base["ConnectionStringName"]; }

        }

        public string ConnectionString
        {

            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
                }
                catch (Exception ex)
                {

                    throw new Exception("Connection string " + ConnectionStringName + " was not found in web.config" + ex.Message);
                }
            }
        }
    }
}