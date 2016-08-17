using MySql.Data.MySqlClient;
using RelatorioFinanceiroV5.Models;
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
                string query = "select sum(quantidade) as total from quantidades a inner join editoras b on a.id_editora = b.id inner join grupos c on b.id_grupo = c.id where b.ativo = 1 and c.ativo = 1 and a.mes_referencia = " + "'" + mesReferencia + "'";
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(ds, "quantidades");
                Debug.WriteLine("");
                dt = ds.Tables[0];
                Debug.WriteLine(dt.Rows[0].Field<decimal>("total").GetType());
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
                //string query = "select floor(quantidade) as quantidade from referenciaxmaisacessados where id_grupo = " + idGrupo + " and mes_referencia = " + "'" + mesReferencia + "'";
                string query = "select sum(quantidade) from mais_acessados_por_editora a inner join editoras b on a.id_editora=b.id inner join grupos c on b.id_grupo=c.id where b.ativo = 1 and c.ativo=1 and a.mes_referencia=" + "'" + mesReferencia + "'" + " and c.id = " + idGrupo;
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
                string query = "select floor(quantidade) as quantidade from mais_acessados_por_editora a inner join editoras b on a.id_editora = b.id inner join grupos c on b.id_grupo = c.id where b.ativo = 1 and c.ativo = 1 and id_editora = " + idEditora + " and mes_referencia = " + "'" + mesReferencia + "'";
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
                string totalQuery = "select sum(quantidade) from mais_acessados_por_editora a inner join editoras b on a.id_editora = b.id inner join grupos c on b.id_grupo = c.id where b.ativo = 1 and c.ativo = 1 and a.mes_referencia = " + "'" + mesReferencia + "'";
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

                query = @"SELECT 
                            a.id,
                            c.id id_grupo,
                            c.nome,
                            SUM(a.quantidade) quantidade,
                            a.mes_referencia
                        FROM
                            quantidades a
                                INNER JOIN
                            editoras b ON a.id_editora = b.id
                                INNER JOIN
                            grupos c ON b.id_grupo = c.id
                        WHERE
                            b.ativo = 1 AND c.ativo = 1
                                AND a.mes_referencia = " + "'" + mesReferencia + "'" +" GROUP BY c.nome ORDER BY c.id";


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
                string query = "select receita, mes_referencia from receita";
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

        public static DataTable GetValoresBordero(MySqlConnection myConn, string mesReferencia)
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
                var query = string.Format("SELECT  a.editora AS Editora, a.grupo AS Grupo, SUM(a.quantidade) AS Quantidade, SUM(a.percentual) AS Percentual, SUM(a.valor_acumulado) AS Acumulado, SUM(a.valor_total_repasse) AS Valor , SUM(valor_total) AS Total  FROM bordero a INNER JOIN editoras b ON a.ideditora = b.id INNER JOIN grupos c ON a.idgrupo = c.id WHERE b.ativo = 1 AND c.ativo = 1 AND a.mes_referencia = '{0}' GROUP BY a.grupo , a.editora WITH ROLLUP;", mesReferencia);

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

        public static void MakePDF(Editora editora)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='table table-bordered table-striped'><thead><tr><th colspan='2'><img src='http://localhost:50403/images/cabecalho.png'/></th></tr><tr><th colspan='2' style='color: #000000; background-color: #337ab7; font-size: 20px;' class='text-center'>Relatório Financeiro - Nuvem de Livros</th></tr><tr style='background-color: #b9defe'><th width='350'><strong>");
            sb.Append(editora.NomeEditora);
            sb.Append("</strong></th><th width='100'><strong>");
            sb.Append(editora.Mes_Referencia);
            sb.Append("</strong></th></tr></thead><tbody><tr><td colspan='2'><strong>Número de Ítens da Editora</strong></td></tr><tr><td><i>Quantidade de Conteúdos</i></td><td class='text-center'><strong>");
            sb.Append(editora.Quantidade);
            sb.Append("</strong></td></tr><tr><td><i>% da editora do total</i></td><td class='text-center'><strong>");
            sb.Append(Math.Round(editora.Percentual_Quantidade, 2).ToString());
            sb.Append("</strong></td></tr><tr><td colspan='2'><strong>Número de Ítens da Editora</strong></td></tr><tr><td><i>Conteúdo de ref. e mais acessados</i></td><td class='text-center'><strong>");
            sb.Append(editora.Quant_Ref_Mais_Acessados.ToString());
            sb.Append("</strong></td></tr><tr><td><i>% da editora dos 10% mais acessados e referência</i></td><td class='text-center'><strong>");
            sb.Append(Math.Round(editora.Percentual_Ref_Mais_Acessados, 2).ToString());
            sb.Append("</strong></td></tr><tr><td><i>Receita líquida total da Nuvem de Livros</i></td><td class='text-center'><strong>");
            sb.Append(editora.Receita.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"))); ;
            sb.Append("</strong></td></tr><tr><td><i>Receita a ser dividida entre as editoras pelo conteúdo (20%)</i></td><td class='text-center'><strong>");
            sb.Append(editora.Receita20.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
            sb.Append("</strong></td></tr><tr><td><i>Receita a ser dividida entre as editoras pelas obras de referência e mais acessados (10%)</i></td><td class='text-center'><strong>");
            sb.Append(editora.Receita10.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
            sb.Append("</strong></td></tr><tr><td><i>Receita total a ser dividida entre as editoras</i></td><td class='text-center'><strong>");
            sb.Append(editora.ReceitaASerDividida.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
            sb.Append("</strong></td></tr><tr><td><i>Valor a ser repassado para a editora pela quantidade de conteúdos</i></td><td class='text-center'><strong>");
            sb.Append(editora.Valor_Conteudo.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
            sb.Append("</strong></td></tr><tr><td><i>Valor a ser repassado para a editora pelas obras de referência e mais acessados</i></td><td class='text-center'><strong>");
            sb.Append(editora.Valor_Mais_Acessados.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
            sb.Append("</strong></td></tr><tr><td><i>Valor total ser repassado para a editora</i></td><td class='text-center'><strong>");
            sb.Append(editora.Valor_Repasse.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
            sb.Append("</strong></td></tr></tbody></table>");



            string myFile = HttpUtility.HtmlDecode(editora.NomeEditora);

            string myFileName = Service.RemoveAccents(myFile);

            PDFHelper.Export(sb.ToString(), "RelFin_" + editora.Mes_Referencia + "_" + myFileName + ".pdf", "~/Content/bootstrap.css");

        }

    }


}