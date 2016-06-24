using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace RelatorioFinanceiroV5.Classes
{
    public static class Service
    {
        //Popula  DropDownList Mes de Referencia
        public static List<String> getMesReferencia(MySqlConnection myConn)
        {
            
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {
                string query = "select mes from mes_referencia";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                Debug.WriteLine("");
                dt = ds.Tables[0];
                List<String> mesReferencia = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("mes")).ToList();
                return mesReferencia;
            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return null;
            }

        }

        public static int QuantidadeTotal(MySqlConnection myConn, string mesReferencia)
        {
            //Dividir a quantidade da editora sobre a quantidade total e multiplica por 100
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {
                string query = "select sum(floor(quantidade)) total from quantidades a inner join grupos b on a.id_grupo = b.id where b.ativo = 1 and mes_referencia = " + "'" + mesReferencia + "'";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds, "quantidades");
                Debug.WriteLine("");
                dt = ds.Tables[0];
                //Debug.WriteLine(dt.Rows[0].Field<decimal>("total").GetType());
                int quantidadeTotal = Convert.ToInt32(dt.Rows[0].Field<decimal>("total"));
                return quantidadeTotal;
            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return 0;
            }

        }

        //Quantidade Total por editora
        public static int QuantidadeTotalPorEditora(MySqlConnection myConn, string mesReferencia)
        {
            //Dividir a quantidade da editora sobre a quantidade total e multiplica por 100
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {
                string query = "select sum(floor(quantidade)) total from quantidades a inner join grupos b on a.id_grupo = b.id where b.ativo = 1 and mes_referencia = " + "'" + mesReferencia + "'";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds, "quantidades");
                Debug.WriteLine("");
                dt = ds.Tables[0];
                int quantidadeTotal = Convert.ToInt32(dt.Rows[0].Field<decimal>("total"));
                return quantidadeTotal;
            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return 0;
            }

        }

        public static int GetMaisAcessados(MySqlConnection myConn, int idGrupo, string mesReferencia)
        {
            //Dividir a quantidade da editora sobre a quantidade total e multiplica por 100
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {
                string query = "select floor(quantidade) as quantidade from referenciaxmaisacessados where id_grupo = " + idGrupo + " and mes_referencia = " + "'" + mesReferencia + "'";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                dt = ds.Tables[0];
                int quantidadeTotal = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                myConn.Close();
                return quantidadeTotal;
            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return 0;
            }

        }

        //Mais acessados por editora
        public static int GetMaisAcessados(MySqlConnection myConn, string mesReferencia, int idEditora)
        {
            //Dividir a quantidade da editora sobre a quantidade total e multiplica por 100
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {
                string query = "select floor(quantidade) as quantidade from refxmais_acessados_por_editora where id_editora = " + idEditora + " and mes_referencia = " + "'" + mesReferencia + "'";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                dt = ds.Tables[0];
                int quantidadeTotal = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                return quantidadeTotal;
            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return 0;
            }

        }

        public static int TotalReferenciaMaisAcessados(MySqlConnection myConn, string mesReferencia)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {
                //select count(acessos) as 'acessos' from titulos where id = 213 and acessos <> 0 and id_mes_referencia = 1

                //string totalQuery = "select sum(quantidade) total from referenciaxmaisacessados";

                string totalQuery = "select sum(quantidade) total from referenciaxmaisacessados where mes_referencia = " + "'" + mesReferencia + "'";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(totalQuery, myConn);
                myAdapter.Fill(ds);
                dt = ds.Tables[0];
                myConn.Close();
                int TotalConteudosReferenciaMaisAcessados = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                myConn.Close();
                return TotalConteudosReferenciaMaisAcessados;

            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return 0;
            }
        }

        public static int TotalReferenciaMaisAcessadosPorEditora(MySqlConnection myConn, string mesReferencia)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {
                //select count(acessos) as 'acessos' from titulos where id = 213 and acessos <> 0 and id_mes_referencia = 1

                //string totalQuery = "select sum(quantidade) total from referenciaxmaisacessados";

                string totalQuery = "select sum(quantidade) total from refxmais_acessados_por_editora";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(totalQuery, myConn);
                myAdapter.Fill(ds);
                dt = ds.Tables[0];
                myConn.Close();
                int TotalConteudosReferenciaMaisAcessados = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                return TotalConteudosReferenciaMaisAcessados;

            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return 0;
            }
        }

        public static decimal GetReceita(MySqlConnection myConn, string mes_referencia)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {
                string query = "select receita from receita where mes_referencia = " + "'" + mes_referencia + "'";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                dt = ds.Tables[0];
                Debug.WriteLine(dt.Rows[0].Field<decimal>("receita").GetType());
                decimal receita = dt.Rows[0].Field<decimal>("receita");
                myConn.Close();
                return receita;

            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return 0;
            }
        }

        public static decimal GetReceitaADividir(MySqlConnection myConn, string mes_referencia)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {
                string query = "select receita_a_ser_dividida as receita from receita where mes_referencia = " + "'" + mes_referencia + "'";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                dt = ds.Tables[0];
                //Debug.WriteLine(dt.Rows[0].Field<decimal>("receita").GetType());
                decimal receita = dt.Rows[0].Field<decimal>("receita");
                myConn.Close();
                return receita;

            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return 0;
            }
        }


        //Popula o GridView do RelatorioPorGrupo.aspx
        public static DataTable getQuantidadeConteudoPorGrupo(string mesReferencia, MySqlConnection myConn)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string query = null;
            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {

                query = "select a.id, b.nome, b.id id_grupo, sum(a.quantidade)quantidade, a.mes_referencia from quantidades a inner join grupos b on a.id_grupo = b.id where b.ativo=1 and a.mes_referencia = " + "'" + mesReferencia + "'" + " group by b.nome order by b.id";


                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                Debug.WriteLine("");
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    Debug.WriteLine("Falha no acesso ao banco...");
                    return null;
                }

            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return null;
            }
        }

        //Popula grid do RelatorioPorEditora.aspx
        public static DataTable getQuantidadeConteudoPorEditora(string mesReferencia, MySqlConnection myConn)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string query = null;
            try
            {
                myConn.Open();
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (myConn.State == System.Data.ConnectionState.Open)
            {

                query = "select a.id, a.editora, a.id_editora, b.id, a.quantidade, a.mes_referencia from quantidades a inner join grupos b on a.id_grupo = b.id where b.ativo = 1 and a.mes_referencia = " + "'" + mesReferencia + "'" + " order by editora";


                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                Debug.WriteLine("");
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    Debug.WriteLine("Falha no acesso ao banco...");
                    return null;
                }

            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return null;
            }
        }


        public static string RemoveAccents(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

    }
}