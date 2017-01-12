using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RelatorioFinanceiroV5.Classes.Database
{
    public abstract class Database
    {
        public string connectionString;

        #region Abstract Functions

        public abstract IDbConnection CreateConnection();
        public abstract IDbCommand CreateCommand();
        public abstract IDbConnection CreateOpenConnection();
        public abstract IDbCommand CreateCommand(string commandText, IDbConnection connection);
        
        #endregion

    }
}