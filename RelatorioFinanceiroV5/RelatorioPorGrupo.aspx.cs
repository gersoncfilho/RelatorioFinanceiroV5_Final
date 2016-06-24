using MySql.Data.MySqlClient;
using RelatorioFinanceiroV5.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RelatorioFinanceiroV5
{
    public partial class RelatorioPorGrupo : System.Web.UI.Page
    {
        private decimal _totalPercentual = 0m;
        private int _quantidadeTotal = 0;
        int _quantTotal = 0;
        private int _totalRefxMaisAcessados = 0;
        private int _idGrupo = 0;
        private decimal _percentualReferenciaMaisAcessado = 0m;
        private decimal _valorPorQuantidade = 0m;
        private decimal _valorPorAcesso = 0m;
        private decimal _valorTotal = 0m;
        private const int round = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            _quantidadeTotal = Service.QuantidadeTotal(myConn, "Jan_16");

            if (!this.IsPostBack)
            {
                ddlMesReferencia.DataSource = Service.getMesReferencia(myConn);
                ddlMesReferencia.DataBind();
                BindGrid("Jan_16", myConn);
                pnlBodyOld.Visible = true;
                myConn.Close();
            }
        }

        private void BindGrid(string mesReferencia, MySqlConnection conn)
        {

            DataTable dt = new DataTable();
            using (conn)
            {
                dt = Service.getQuantidadeConteudoPorGrupo(mesReferencia, conn);


                using (dt)
                {

                    GridViewQuantidades.DataSource = dt;
                    GridViewQuantidades.DataBind();
                    GridViewQuantidades.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                }
            }
        }

        protected void GridViewQuantidadesOld_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string _mesReferencia = e.Row.Cells[1].Text;
            var myConn = Connection.conn();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                _idGrupo = Convert.ToInt32(e.Row.Cells[9].Text);
                decimal receita = Service.GetReceita(myConn, _mesReferencia);
                decimal receita20 = Math.Round((decimal)receita * (decimal)0.2, round);
                decimal receita10 = Math.Round((decimal)receita * (decimal)0.1, round);
                int quantidade = Convert.ToInt32(e.Row.Cells[2].Text);
                decimal percentual = Math.Round(quantidade / (decimal)_quantidadeTotal * 100, round);
                int maisAcessados = Service.GetMaisAcessados(myConn, _idGrupo, _mesReferencia);
                int totalRefxMaisAcessados = Service.TotalReferenciaMaisAcessados(myConn, _mesReferencia);
                decimal percentualReferenciaMaisAcessado = Math.Round(Convert.ToDecimal(maisAcessados / (decimal)totalRefxMaisAcessados * 100), round);
                decimal valorPorQuantidade = (percentual * receita20) / 100;
                decimal valorPorAcesso = (percentualReferenciaMaisAcessado * receita10) / 100;
                decimal valorTotal = valorPorQuantidade + valorPorAcesso;

                e.Row.Cells[3].Text = percentual.ToString();
                e.Row.Cells[4].Text = maisAcessados.ToString();
                e.Row.Cells[5].Text = percentualReferenciaMaisAcessado.ToString();
                e.Row.Cells[6].Text = valorPorQuantidade.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));
                e.Row.Cells[7].Text = valorPorAcesso.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));
                e.Row.Cells[8].Text = valorTotal.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));

                _totalPercentual = _totalPercentual + percentual;
                _quantTotal = _quantTotal + quantidade;
                _totalRefxMaisAcessados = _totalRefxMaisAcessados + maisAcessados;
                _percentualReferenciaMaisAcessado = _percentualReferenciaMaisAcessado + percentualReferenciaMaisAcessado;
                _valorPorAcesso = _valorPorAcesso + valorPorAcesso;
                _valorPorQuantidade = _valorPorQuantidade + valorPorQuantidade;
                _valorTotal = _valorTotal + valorTotal;

                Debug.WriteLine(e.Row.Cells[0].Text);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = _quantTotal.ToString();
                e.Row.Cells[3].Text = Math.Round(_totalPercentual, 2).ToString();
                e.Row.Cells[4].Text = _totalRefxMaisAcessados.ToString();
                e.Row.Cells[5].Text = Math.Round(_percentualReferenciaMaisAcessado, 2).ToString();
                e.Row.Cells[6].Text = _valorPorQuantidade.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")); ;
                e.Row.Cells[7].Text = _valorPorAcesso.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")); ;
                e.Row.Cells[8].Text = _valorTotal.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR")); ;
            }
        }

        protected void GridViewQuantidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Visualizar")
            {
                var myConn = Connection.conn();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewQuantidades.Rows[index];

                int idGrupo = (int)Convert.ToInt32(row.Cells[9].Text);
                string mesReferencia = row.Cells[1].Text;

                //calcula o percentual da quantidade sobre o total de conteudos

                int quantidadeTotal = Service.QuantidadeTotal(myConn, mesReferencia);
                //decimal percentual = Math.Round(Convert.ToDecimal(row.Cells[2].Text) / (decimal)quantidadeTotal * 100, 6);
                decimal percentual = Math.Round(Convert.ToDecimal(row.Cells[2].Text) / (decimal)_quantidadeTotal * 100, round);

                lblPercentualEditoraTotal.Text = Math.Round(percentual, 2).ToString() + "%";

                int maisAcessados = Service.GetMaisAcessados(myConn, idGrupo, mesReferencia);
                int totalRefxMaisAcessados = Service.TotalReferenciaMaisAcessados(myConn, mesReferencia);
                lblTotalRefMaisAcessados.Text = maisAcessados.ToString();

                decimal percentualReferenciaMaisAcessado = Math.Round(Convert.ToDecimal(maisAcessados / (decimal)totalRefxMaisAcessados * 100), 5);

                lblPercentual10MaisAcessados.Text = Math.Round(percentualReferenciaMaisAcessado, 2).ToString() + "%";



                decimal receita = Service.GetReceita(myConn, mesReferencia);
                lblReceita.Text = receita.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));


                decimal receita20 = Math.Round((decimal)receita * (decimal)0.2, 6);
                decimal receita10 = Math.Round((decimal)receita * (decimal)0.1, 6);


                lblReceita_20.Text = receita20.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));
                lblReceita_10.Text = receita10.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));

                decimal receitaTotal = receita10 + receita20;
                lblReceitaTotalASerDividida.Text = receitaTotal.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));

                //ok ate aqui

                double valorASerRepassadoPelaQuantidade = Math.Round((Convert.ToDouble(percentual) * Convert.ToDouble(receita20)) / 100, 5);
                lblValorRepasseQuantidade.Text = valorASerRepassadoPelaQuantidade.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));

                double valorASerRepassadoPelaReferMaisAcessados = Math.Round((Convert.ToDouble(percentualReferenciaMaisAcessado) * Convert.ToDouble(receita10)) / 100, 5);
                lblValorRepasseRefMaisAcessados.Text = valorASerRepassadoPelaReferMaisAcessados.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));

                double valorTotalRepasse = valorASerRepassadoPelaQuantidade + valorASerRepassadoPelaReferMaisAcessados;
                lblValorTotalRepasse.Text = valorTotalRepasse.ToString("C2", CultureInfo.CreateSpecificCulture("pt-BR"));

                lblGrupo.Text = row.Cells[0].Text;
                lblMes.Text = row.Cells[1].Text;
                lblQuantidadeConteudos.Text = row.Cells[2].Text;

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "openModal();", true);
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            MakePDF();
        }


        private void MakePDF(string grupo, string mes, int quantidade, decimal percentualEditora, int totalMaisAcessados, decimal percentualMaisAcessados, decimal receita, decimal receita10, decimal receita20, decimal receitaASerDividida, string repasseQuantidade, string repasseMaisAcessados, string valorTotalRepasse)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='table table-bordered table-striped'><thead><tr><th colspan='2'><img src='http://localhost:50403/images/cabecalho.png'/></th></tr><tr><th colspan='2' style='color: #000000; background-color: #337ab7; font-size: 20px;' class='text-center'>Relatório Financeiro - Nuvem de Livros</th></tr><tr style='background-color: #b9defe'><th width='350'><strong>");
                    sb.Append(grupo);
                    sb.Append("</strong></th><th width='100'><strong>");
                    sb.Append(mes);
                    sb.Append("</strong></th></tr></thead><tbody><tr><td colspan='2'><strong>Número de Ítens da Editora</strong></td></tr><tr><td><i>Quantidade de Conteúdos</i></td><td class='text-center'><strong>");
                    sb.Append(quantidade);
                    sb.Append("</strong></td></tr><tr><td><i>% da editora do total</i></td><td class='text-center'><strong>");
                    sb.Append(percentualEditora);
                    sb.Append("</strong></td></tr><tr><td colspan='2'><strong>Número de Ítens da Editora</strong></td></tr><tr><td><i>Conteúdo de ref. e mais acessados</i></td><td class='text-center'><strong>");
                    sb.Append(totalMaisAcessados);
                    sb.Append("</strong></td></tr><tr><td><i>% da editora dos 10% mais acessados e referência</i></td><td class='text-center'><strong>");
                    sb.Append(percentualMaisAcessados);
                    sb.Append("</strong></td></tr><tr><td><i>Receita líquida total da Nuvem de Livros</i></td><td class='text-center'><strong>");
                    sb.Append(receita);
                    sb.Append("</strong></td></tr><tr><td><i>Receita a ser dividida entre as editoras pelo conteúdo (20%)</i></td><td class='text-center'><strong>");
                    sb.Append(receita20);
                    sb.Append("</strong></td></tr><tr><td><i>Receita a ser dividida entre as editoras pelas obras de referência e mais acessados (10%)</i></td><td class='text-center'><strong>");
                    sb.Append(receita10);
                    sb.Append("</strong></td></tr><tr><td><i>Receita total a ser dividida entre as editoras</i></td><td class='text-center'><strong>");
                    sb.Append(receitaASerDividida);
                    sb.Append("</strong></td></tr><tr><td><i>Valor a ser repassado para a editora pela quantidade de conteúdos</i></td><td class='text-center'><strong>");
                    sb.Append(lblValorRepasseQuantidade);
                    sb.Append("</strong></td></tr><tr><td><i>Valor a ser repassado para a editora pelas obras de referência e mais acessados</i></td><td class='text-center'><strong>");
                    sb.Append(lblValorRepasseRefMaisAcessados);
                    sb.Append("</strong></td></tr><tr><td><i>Valor total ser repassado para a editora</i></td><td class='text-center'><strong>");
                    sb.Append(valorTotalRepasse);
                    sb.Append("</strong></td></tr></tbody></table>");



                    string myFile = HttpUtility.HtmlDecode(grupo);

                    string myFileName = Service.RemoveAccents(myFile);

                    PDFHelper.Export(sb.ToString(), "RelFin_" + mes + "_" + myFileName + ".pdf", "~/Content/bootstrap.css");

                }
            }
        }

        private void MakePDF()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='table table-bordered table-striped'><thead><tr><th colspan='2'><img src='http://localhost:50403/images/cabecalho.png'/></th></tr><tr><th colspan='2' style='color: #000000; background-color: #337ab7; font-size: 20px;' class='text-center'>Relatório Financeiro - Nuvem de Livros</th></tr><tr style='background-color: #b9defe'><th width='350'><strong>");
                    sb.Append(lblGrupo.Text);
                    sb.Append("</strong></th><th width='100'><strong>");
                    sb.Append(lblMes.Text);
                    sb.Append("</strong></th></tr></thead><tbody><tr><td colspan='2'><strong>Número de Ítens da Editora</strong></td></tr><tr><td><i>Quantidade de Conteúdos</i></td><td class='text-center'><strong>");
                    sb.Append(lblQuantidadeConteudos.Text);
                    sb.Append("</strong></td></tr><tr><td><i>% da editora do total</i></td><td class='text-center'><strong>");
                    sb.Append(lblPercentualEditoraTotal.Text);
                    sb.Append("</strong></td></tr><tr><td colspan='2'><strong>Número de Ítens da Editora</strong></td></tr><tr><td><i>Conteúdo de ref. e mais acessados</i></td><td class='text-center'><strong>");
                    sb.Append(lblTotalRefMaisAcessados.Text);
                    sb.Append("</strong></td></tr><tr><td><i>% da editora dos 10% mais acessados e referência</i></td><td class='text-center'><strong>");
                    sb.Append(lblPercentual10MaisAcessados.Text);
                    sb.Append("</strong></td></tr><tr><td><i>Receita líquida total da Nuvem de Livros</i></td><td class='text-center'><strong>");
                    sb.Append(lblReceita.Text);
                    sb.Append("</strong></td></tr><tr><td><i>Receita a ser dividida entre as editoras pelo conteúdo (20%)</i></td><td class='text-center'><strong>");
                    sb.Append(lblReceita_20.Text);
                    sb.Append("</strong></td></tr><tr><td><i>Receita a ser dividida entre as editoras pelas obras de referência e mais acessados (10%)</i></td><td class='text-center'><strong>");
                    sb.Append(lblReceita_10.Text);
                    sb.Append("</strong></td></tr><tr><td><i>Receita total a ser dividida entre as editoras</i></td><td class='text-center'><strong>");
                    sb.Append(lblReceitaTotalASerDividida.Text);
                    sb.Append("</strong></td></tr><tr><td><i>Valor a ser repassado para a editora pela quantidade de conteúdos</i></td><td class='text-center'><strong>");
                    sb.Append(lblValorRepasseQuantidade.Text);
                    sb.Append("</strong></td></tr><tr><td><i>Valor a ser repassado para a editora pelas obras de referência e mais acessados</i></td><td class='text-center'><strong>");
                    sb.Append(lblValorRepasseRefMaisAcessados.Text);
                    sb.Append("</strong></td></tr><tr><td><i>Valor total ser repassado para a editora</i></td><td class='text-center'><strong>");
                    sb.Append(lblValorTotalRepasse.Text);
                    sb.Append("</strong></td></tr></tbody></table>");



                    string myFile = HttpUtility.HtmlDecode(lblGrupo.Text);

                    string myFileName = Service.RemoveAccents(myFile);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "closeModal();", true);

                    PDFHelper.Export(sb.ToString(), "RelFin_" + lblMes.Text + "_" + myFileName + ".pdf", "~/Content/bootstrap.css");

                }
            }
        }

        protected void btnOK_OnClick(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            var _mes_referencia = ddlMesReferencia.SelectedValue;
            Debug.WriteLine(_mes_referencia);
            BindGrid(_mes_referencia, myConn);
            _totalPercentual = Service.QuantidadeTotal(myConn, _mes_referencia);
        }

        protected void btnGerarPDFs_OnClick(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            string _grupo = "";
            string _mes = "";
            int _quantidade = 0;
            decimal _percentualEditora = 0.0m;
            int _totalMaisAcessados = 0;
            decimal _percentualMaisAcessados = 0.0m;
            decimal _receita = 0.0m;
            decimal _receita10 = 0.0m;
            decimal _receita20 = 0.0m;
            decimal _receitaASerDividida = 0.0m;
            string _repasseQuantidade = "";
            string _repasseMaisAcessados = "";
            string _valorTotalRepasse = "";
            GridView grd = GridViewQuantidades;
            foreach (GridViewRow row in grd.Rows)
            {
                Debug.WriteLine(grd.Rows.Count);
                _grupo = row.Cells[0].Text;
                _mes = row.Cells[1].Text;
                _quantidade = Convert.ToInt32(row.Cells[2].Text);
                _percentualEditora = Convert.ToDecimal(row.Cells[3].Text);
                _totalMaisAcessados = Convert.ToInt32(row.Cells[4].Text);
                _percentualMaisAcessados = Convert.ToDecimal(row.Cells[5].Text);
                _receita = Service.GetReceita(myConn, _mes);
                _receita10 = Math.Round((decimal)_receita * (decimal)0.1, 6);
                _receita20 = Math.Round((decimal)_receita * (decimal)0.2, 6);
                _receitaASerDividida = Service.GetReceitaADividir(myConn, _mes);
                _repasseQuantidade = row.Cells[6].Text;
                _repasseMaisAcessados = row.Cells[7].Text;
                _valorTotalRepasse = row.Cells[8].Text;

                Thread myThread = new Thread(() => this.MakePDF(_grupo, _mes, _quantidade, _percentualEditora, _totalMaisAcessados, _percentualMaisAcessados, _receita, _receita10, _receita20, _receitaASerDividida, _repasseQuantidade, _repasseMaisAcessados, _valorTotalRepasse));

                myThread.Start();


            }
        }

        #region Methods
        private decimal getPercentualPorQuantidade(MySqlConnection myConn, string mes_referencia, int quantidade)
        {
            int quantidadeTotal = Service.QuantidadeTotal(myConn, mes_referencia);
            decimal percentual = Math.Round(quantidade / (decimal)quantidadeTotal * 100, 6);
            return percentual;

        }


        #endregion

    }
}