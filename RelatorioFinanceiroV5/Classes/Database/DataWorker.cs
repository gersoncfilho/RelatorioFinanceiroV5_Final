using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelatorioFinanceiroV5.Classes.Database
{
    public class DataWorker
    {
        private static Database _database = null;

        static DataWorker()
        {

            try
            {
                _database = DbFactory.CreateDatabase();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static Database database
        {
            get { return _database; }
        }
    }
}