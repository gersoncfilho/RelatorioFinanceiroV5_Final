﻿using MySql.Data.MySqlClient;
using RelatorioFinanceiroV5.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

        public static decimal GetReceita(MySqlConnection myConn, string mes_referencia, int classificacao)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string query;
            

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

                query = string.Format("select receita from receita where mes_referencia = '{0}' and internacional = '{1}'", mes_referencia, classificacao);
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
        public static DataTable getQuantidadeConteudoPorGrupo(string mesReferencia, int classificacao, MySqlConnection myConn)
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

                query = string.Format(@"SELECT 
                            idGrupo,
                            mes_referencia,
                            grupo,
                            SUM(quantidade) quantidade,
                            SUM(percentual) percentual,
                            SUM(quantidaderefxmaisacessados) quantidademaisacessados,
                            SUM(percentualmaisacessados) percentualmaisacessados,
                            SUM(valorconteudo) valorconteudo,
                            SUM(valormaisacessados) valormaisacessados,
                            SUM(valor_total_repasse) valortotalrepasse,
                            idGrupo,
                            pdfOk,
                            internacional                         
                        FROM
                            bordero WHERE mes_referencia = '{0}' AND internacional = '{1}' GROUP BY grupo ORDER BY idGrupo", mesReferencia, classificacao);


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
        public static DataTable getQuantidadeConteudoPorEditora(string mesReferencia, int classificacao, MySqlConnection myConn)
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


                query = string.Format("SELECT mes_referencia, editora, quantidade, percentual,  quantidaderefxmaisacessados,  percentualmaisacessados, valorconteudo, valormaisacessados, valor_total_repasse, idEditora, idGrupo FROM bordero WHERE mes_referencia = '{0}' AND internacional = '{1}' ORDER BY idGrupo", mesReferencia, classificacao);
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
                var query = string.Format("SELECT  a.editora AS Editora, a.grupo AS Grupo, SUM(a.quantidade) AS Quantidade, SUM(a.percentual) AS Percentual, SUM(a.quantidaderefxmaisacessados) AS RefXMaisAcessados, SUM(a.percentualmaisacessados) AS PercentualMaisAcessados , SUM(a.valorconteudo) AS ValorConteudo, SUM(a.valormaisacessados)AS ValorMaisAcessados, SUM(a.valor_total_repasse) AS ValorTotal  FROM bordero a INNER JOIN editoras b ON a.ideditora = b.id INNER JOIN grupos c ON a.idgrupo = c.id WHERE b.ativo = 1 AND c.ativo = 1 AND a.mes_referencia = '{0}' GROUP BY a.grupo , a.editora WITH ROLLUP;", mesReferencia);

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

        public static bool VerificaPDFImpresso(int idGrupo, string mesReferencia)
        {
            var myConn = Connection.conn();
            try
            {
                myConn.Open();
                var query = string.Format("select pdfOk from bordero where mes_referencia = '{0}' and idGrupo = '{1}'", mesReferencia, idGrupo);
                MySqlCommand cmd = new MySqlCommand(query, myConn);

                int result = Convert.ToInt32(cmd.ExecuteScalar());
                myConn.Close();

                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("MySql Error: " + ex.Message);
                myConn.Close();
                return false;
            }

        }

        //public static void MakePDF(PDFGrupo pdfGrupo)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<table class='table table-bordered table-striped'><thead><tr><th colspan='2'><img src='http://localhost:50403/images/cabecalho.png'/></th></tr><tr><th colspan='2' style='color: #000000; background-color: #337ab7; font-size: 20px;' class='text-center'>Relatório Financeiro - Nuvem de Livros</th></tr><tr style='background-color: #b9defe'><th width='350'><strong>");
        //    sb.Append(pdfGrupo.Grupo);
        //    sb.Append("</strong></th><th width='100'><strong>");
        //    sb.Append(pdfGrupo.MesReferencia);
        //    sb.Append("</strong></th></tr></thead><tbody><tr><td colspan='2'><strong>Número de Ítens da Editora</strong></td></tr><tr><td><i>Quantidade de Conteúdos</i></td><td class='text-center'><strong>");
        //    sb.Append(pdfGrupo.Quantidade);
        //    sb.Append("</strong></td></tr><tr><td><i>% da editora do total</i></td><td class='text-center'><strong>");
        //    sb.Append(Math.Round(pdfGrupo.Percentual, 2).ToString());
        //    sb.Append("</strong></td></tr><tr><td colspan='2'><strong>Número de Ítens da Editora</strong></td></tr><tr><td><i>Conteúdo de ref. e mais acessados</i></td><td class='text-center'><strong>");
        //    sb.Append(pdfGrupo.QuantidadeMaisAcessados.ToString());
        //    sb.Append("</strong></td></tr><tr><td><i>% da editora dos 10% mais acessados e referência</i></td><td class='text-center'><strong>");
        //    sb.Append(Math.Round(pdfGrupo.PercentualMaisAcessados, 2).ToString());
        //    sb.Append("</strong></td></tr><tr><td><i>Receita líquida total da Nuvem de Livros</i></td><td class='text-center'><strong>");
        //    //sb.Append(grupo.Receita.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"))); ;
        //    //sb.Append("</strong></td></tr><tr><td><i>Receita a ser dividida entre as editoras pelo conteúdo (20%)</i></td><td class='text-center'><strong>");
        //    //sb.Append(grupo.Receita20.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
        //    //sb.Append("</strong></td></tr><tr><td><i>Receita a ser dividida entre as editoras pelas obras de referência e mais acessados (10%)</i></td><td class='text-center'><strong>");
        //    //sb.Append(editora.Receita10.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
        //    //sb.Append("</strong></td></tr><tr><td><i>Receita total a ser dividida entre as editoras</i></td><td class='text-center'><strong>");
        //    //sb.Append(editora.ReceitaASerDividida.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
        //    sb.Append("</strong></td></tr><tr><td><i>Valor a ser repassado para a editora pela quantidade de conteúdos</i></td><td class='text-center'><strong>");
        //    sb.Append(pdfGrupo.ValorConteudo.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
        //    sb.Append("</strong></td></tr><tr><td><i>Valor a ser repassado para a editora pelas obras de referência e mais acessados</i></td><td class='text-center'><strong>");
        //    sb.Append(pdfGrupo.ValorMaisAcessados.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
        //    sb.Append("</strong></td></tr><tr><td><i>Valor total ser repassado para a editora</i></td><td class='text-center'><strong>");
        //    sb.Append(pdfGrupo.ValorTotalRepasse.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")));
        //    sb.Append("</strong></td></tr></tbody></table>");

        //    string myFile = HttpUtility.HtmlDecode(pdfGrupo.Grupo);

        //    string myFileName = Service.RemoveAccents(myFile);

        //    PDFHelper.Export(sb.ToString(), "RelFin_" + pdfGrupo.MesReferencia + "_" + myFileName + ".pdf", "~/Content/bootstrap.css");

        //}

        public static DataTable GetGrupos(MySqlConnection myConn)
        {
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
                var query = string.Format("SELECT * FROM grupos WHERE ativo = 1");
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(dt);
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

        public static DataTable GetAcessos(MySqlConnection myConn, int idGrupo)
        {
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
                var query = string.Format("SELECT * FROM acesso WHERE idGrupo = {0}", idGrupo);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(query, myConn);
                myAdapter.Fill(dt);
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

        public static string GetNomeGrupo(int idGrupo)
        {
            var myConn = Connection.conn();
            try
            {
                myConn.Open();
                var query = string.Format("select nome from grupos where id = {0}", idGrupo);
                MySqlCommand cmd = new MySqlCommand(query, myConn);

                string result = Convert.ToString(cmd.ExecuteScalar());
                myConn.Close();

                return result;

            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("MySql Error: " + ex.Message);
                myConn.Close();
                return null;
            }

        }

    }


}