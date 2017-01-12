using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RelatorioFinanceiroV5.Classes.Database
{
    public class DbFactory
    {
        public static DatabaseFactorySectionHandler sectionHandler = (DatabaseFactorySectionHandler)ConfigurationManager.GetSection("DatabaseFactoryConfiguration");

        private DbFactory()
        {

        }

        public static Database CreateDatabase()
        {
            if (sectionHandler.Name.Length == 0)
            {
                throw new Exception("Database name not defined in DatabaseFactoryConfiguration section of web.config");
            }

            try
            {
                //Find the class
                Type database = Type.GetType(sectionHandler.Name);

                //Get constructor
                ConstructorInfo constructor = database.GetConstructor(new Type[] { });

                //Invoke constructor
                Database createdObject = (Database)constructor.Invoke(null);

                //Initialize connection
                createdObject.connectionString = sectionHandler.ConnectionString;

                //Pass back the instance of Database
                return createdObject;
            }
            catch (Exception ex)
            {

                throw new Exception("Error instantiate database " + sectionHandler.Name + ". " + ex.Message);
            }

        }

    }
}