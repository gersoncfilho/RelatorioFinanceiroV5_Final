using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RelatorioFinanceiroV5.Classes.Database
{
    public class MySqlDatabase : Database
    {
        public override IDbConnection CreateOpenConnection()
        {
            MySqlConnection connection = (MySqlConnection)CreateConnection();
            connection.Open();

            return connection;
        }

        public override IDbCommand CreateCommand()
        {
            return new MySqlCommand();
        }

        public override IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            MySqlCommand command = (MySqlCommand)CreateCommand();
            command.CommandText = commandText;
            command.Connection = (MySqlConnection) connection;
            command.CommandType = CommandType.Text;

            return command;
        }

        public override IDbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }

        
    }
}