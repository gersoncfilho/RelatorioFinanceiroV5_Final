using RelatorioFinanceiroV5.Classes.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RelatorioFinanceiroV5.Classes.Service
{
    public class DbService : DataWorker
    {
        public static List<string> LoadMesReferencia()
        {
            using (IDbConnection connection = database.CreateOpenConnection())
            {
                using (IDbCommand command = database.CreateCommand("select mes from mes_referencia", connection))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        List<String> mesReferencia = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("mes")).ToList();
                        return mesReferencia;
                    }
                }
            }
        }
    }
}