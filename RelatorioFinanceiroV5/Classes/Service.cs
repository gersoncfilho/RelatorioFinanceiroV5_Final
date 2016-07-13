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
                string query = "select nome_mes from mes_referencia";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                Debug.WriteLine("");
                dt = ds.Tables[0];
                List<String> mesReferencia = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("nome_mes")).ToList();
                return mesReferencia;
            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return null;
            }

        }

        public static int QuantidadeTotal(MySqlConnection myConn, int idMesReferencia)
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
                string query = @"SELECT SUM(quantidade) AS total 
                                    FROM quantidades a 
                                INNER JOIN editoras b 
                                    ON a.id_editora = b.id_editora 
                                inner join grupos c 
                                    on b.id_grupo = c.id_grupo 
                                INNER JOIN mes_referencia d ON a.id_mes = d.id_mes                           
                                where b.ativo = 1 and c.ativo = 1 and a.id_mes = " + idMesReferencia;
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds, "quantidades");
                Debug.WriteLine("");
                dt = ds.Tables[0];
                Debug.WriteLine(dt.Rows[0].Field<decimal>("total").GetType());
                int quantidadeTotal = Convert.ToInt32(dt.Rows[0].Field<decimal>("total"));
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


        public static int GetMaisAcessados(MySqlConnection myConn, int idGrupo, int idMesReferencia)
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
                //string query = "select floor(quantidade) as quantidade from referenciaxmaisacessados where id_grupo = " + idGrupo + " and mes_referencia = " + "'" + mesReferencia + "'";
                string query = "select sum(quantidade) from refxmais_acessados_por_editora a inner join editoras b on a.id_editora=b.id inner join grupos c on b.id_grupo=c.id where b.ativo = 1 and c.ativo=1 and a.mes_referencia=" + "'" + idMesReferencia + "'" + " and c.id = " + idGrupo;
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows[0].ItemArray[0] != DBNull.Value)
                {
                    int quantidadeTotal = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                    myConn.Close();
                    return quantidadeTotal;
                }
                else
                {
                    return 0;
                }

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
                string query = "select floor(quantidade) as quantidade from refxmais_acessados_por_editora a inner join editoras b on a.id_editora = b.id inner join grupos c on b.id_grupo = c.id where b.ativo = 1 and c.ativo = 1 and id_editora = " + idEditora + " and mes_referencia = " + "'" + mesReferencia + "'";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int quantidadeTotal = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                    return quantidadeTotal;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return 0;
            }

        }

        public static int TotalReferenciaMaisAcessados(MySqlConnection myConn, int idMesReferencia)
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
                string totalQuery = "select sum(quantidade) from refxmais_acessados_por_editora a inner join editoras b on a.id_editora = b.id inner join grupos c on b.id_grupo = c.id where b.ativo = 1 and c.ativo = 1 and a.mes_referencia = " + "'" + idMesReferencia + "'";
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



        public static decimal GetReceita(MySqlConnection myConn, int idMesReferencia)
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
                string query = "select receita from receita where mes_referencia = " + "'" + idMesReferencia + "'";
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

        public static decimal GetReceitaADividir(MySqlConnection myConn, int idMesReferencia)
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
                string query = "select receita_a_ser_dividida as receita from receita where mes_referencia = " + "'" + idMesReferencia + "'";
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
        public static DataTable getQuantidadeConteudoPorGrupo(int idMesReferencia, MySqlConnection myConn)
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

                //query = "SELECT  a.id_quantidades, c.nome_grupo, c.id_grupo as id_grupo, SUM(a.quantidade) quantidade, a.id_mes  FROM quantidades a INNER JOIN editoras b ON a.id_editora = b.id_editora INNER JOIN  grupos c ON b.id_grupo = c.id_grupo WHERE b.ativo = 1 AND c.ativo = 1 AND a.id_mes = " + idMesReferencia + " GROUP BY c.nome_grupo ORDER BY c.id_grupo";
                query = "select c.nome_grupo, d.nome_mes, sum(quantidade), a.id_editora, a.id_grupo, a.id_mes, c.ativo from quantidades a inner join editoras b on a.id_editora=b.id_editora inner join grupos c on a.id_grupo=c.id_grupo inner join mes_referencia d on a.id_mes=d.id_mes where b.ativo=1 and c.ativo=1 and a.id_mes=3 group by a.id_grupo order by a.id_grupo ";

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

                //query = "select a.id, a.editora, a.id_editora, b.id, a.quantidade, a.mes_referencia from quantidades a inner join grupos b on a.id_grupo = b.id where b.ativo = 1 and a.mes_referencia = " + "'" + mesReferencia + "'" + " order by editora";
                query = "select a.id, b.nome, b.id id_editora, a.quantidade, a.mes_referencia from quantidades a inner join editoras b on a.id_editora = b.id inner join grupos c on b.id_grupo = c.id where b.ativo=1 and c.ativo=1 and a.mes_referencia = " + "'" + mesReferencia + "'" + " order by a.id_grupo";

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

        public static DataTable GetReceitaGrafico(MySqlConnection myConn)
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
                string query = @"select 
	                                b.nome_mes, a.receita, 
	                                a.receita_a_ser_dividida 
                                from 
	                                receita a 
		                                inner join 
	                                mes_referencia b on a.id_mes = b.id_mes";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                dt = ds.Tables[0];

                myConn.Close();
                return dt;

            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return null;
            }
        }

        public static DataTable GetValoresBordero(MySqlConnection myConn, int idMesReferencia)
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
                //string query = "select * from grupos a  inner join editoras b on a.id = b.id_grupo inner join quantidades c on b.id = c.id_editora INNER JOIN receita d ON c.mes_referencia = d.mes_referencia where a.ativo = 1 and b.ativo = 1 and c.mes_referencia = " + "'" + mesReferencia + "'" + " group by b.nome order by a.nome";
                string query = @"SELECT 
                                    a.id,
                                    b.nome,
                                    a.quantidade,
                                    a.mes_referencia,
                                    d.receita,
                                    d.receita_a_ser_dividida
                                FROM
                                    quantidades a
                                        INNER JOIN
                                    editoras b ON a.id_editora = b.id
                                        INNER JOIN
                                    grupos c ON b.id_grupo = c.id
		                                INNER JOIN
	                                receita d on a.mes_referencia = d.mes_referencia
                                WHERE
                                    b.ativo = 1 AND c.ativo = 1
                                        AND a.mes_referencia = 'Fev_16'
                                ORDER BY a.id_grupo";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds);
                dt = ds.Tables[0];
                myConn.Close();
                return dt;

            }
            else
            {
                Debug.Write("Erro no acesso ao banco");
                myConn.Close();
                return null;
            }
        }

    }


}