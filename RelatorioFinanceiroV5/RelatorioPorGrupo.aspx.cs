using MySql.Data.MySqlClient;
using RelatorioFinanceiroV5.Classes;
using RelatorioFinanceiroV5.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace RelatorioFinanceiroV5
{


    public partial class RelatorioPorGrupo : System.Web.UI.Page
    {

        private string local = "pt-BR";
        
        private decimal _totalPercentual = 0m;
        private int _quantidadeTotal = 0;
        private decimal _percentualTotal = 0m;
        private int _quantidadeMaisTotal = 0;
        private decimal _percentualReferenciaMaisAcessado = 0m;
        private decimal _valorPorQuantidade = 0m;
        private decimal _valorPorAcesso = 0m;
        private decimal _valorTotalRepasse = 0m;
        private const int round = 6;
        //private int _classificacao;
        private List<Grupo> grupos = new List<Grupo>();


        protected void Page_Load(object sender, EventArgs e)
        {

            var myConn = Connection.conn();
            _quantidadeTotal = Service.QuantidadeTotal(myConn, "Fev_16");
            string[] classificacao = { "Nacional", "Internacional" };
            Debug.WriteLine("Quantidade total: " + _quantidadeTotal);

            if (!this.IsPostBack)
            {
                ddlClassificacao.DataSource = classificacao;
                ddlClassificacao.DataBind();

                ddlMesReferencia.DataSource = Service.getMesReferencia(myConn);
                ddlMesReferencia.DataBind();
                BindGrid("Jan_16", 0, myConn);
                pnlBodyOld.Visible = true;
                myConn.Close();
            }
        }

        private void BindGrid(string mesReferencia, int classificacao, MySqlConnection conn)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            using (conn)
            {
                dt = Service.getQuantidadeConteudoPorGrupo(mesReferencia, classificacao, conn);
                GridViewQuantidades.DataSource = dt;
                GridViewQuantidades.DataBind();
            }
        }


        protected void GridViewQuantidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Visualizar")
            {
                var myConn = Connection.conn();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewQuantidades.Rows[index];


                lblGrupo.Text = row.Cells[0].Text;
                lblMes.Text = row.Cells[1].Text;
                lblQuantidadeConteudos.Text = row.Cells[2].Text;
                lblPercentualEditoraTotal.Text = row.Cells[3].Text;
                lblTotalRefMaisAcessados.Text = row.Cells[4].Text;
                lblPercentual10MaisAcessados.Text = row.Cells[5].Text;

                decimal receita = Service.GetReceita(myConn, row.Cells[1].Text, Convert.ToInt16(Session["_classificacao"]));
                if(Convert.ToInt16(Session["_classificacao"]) > 0)
                {
                    local = "en-US";
                }

                lblReceita.Text = receita.ToString("C2", CultureInfo.CreateSpecificCulture(local));


                decimal receita20 = Math.Round((decimal)receita * (decimal)0.2, 6);
                decimal receita10 = Math.Round((decimal)receita * (decimal)0.1, 6);


                lblReceita_20.Text = receita20.ToString("C2", CultureInfo.CreateSpecificCulture(local));
                lblReceita_10.Text = receita10.ToString("C2", CultureInfo.CreateSpecificCulture(local));

                decimal receitaTotal = receita10 + receita20;
                lblReceitaTotalASerDividida.Text = receitaTotal.ToString("C2", CultureInfo.CreateSpecificCulture(local));


                decimal _repasseQuant = Convert.ToDecimal(row.Cells[7].Text);
                lblValorRepasseQuantidade.Text = _repasseQuant.ToString("C2", CultureInfo.CreateSpecificCulture(local));

                decimal _valorRepMaisAce = Convert.ToDecimal(row.Cells[8].Text);
                lblValorRepasseRefMaisAcessados.Text = _valorRepMaisAce.ToString("C2", CultureInfo.CreateSpecificCulture(local));

                decimal _valorTotalRepasse = Convert.ToDecimal(row.Cells[9].Text);
                lblValorTotalRepasse.Text = _valorTotalRepasse.ToString("C2", CultureInfo.CreateSpecificCulture(local));

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "openModal();", true);
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            MakePDF();

        }


        private void MakePDF()
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

            PDFHelper.Export(sb.ToString(), "RelFin_" + lblMes.Text + "_" + myFileName + ".pdf", "~/Content/bootstrap.css");

        }

        protected void btnOK_OnClick(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            var _mes_referencia = ddlMesReferencia.SelectedValue;
            Session["_classificacao"] = ddlClassificacao.SelectedIndex;



            Debug.WriteLine(_mes_referencia);
            BindGrid(_mes_referencia, Convert.ToInt16(Session["_classificacao"]), myConn);
            _totalPercentual = Service.QuantidadeTotal(myConn, _mes_referencia);
        }


        #region Methods
        private decimal getPercentualPorQuantidade(MySqlConnection myConn, string mes_referencia, int quantidade)
        {
            int quantidadeTotal = Service.QuantidadeTotal(myConn, mes_referencia);
            decimal percentual = Math.Round(quantidade / (decimal)quantidadeTotal * 100, 6);
            return percentual;

        }


        #endregion

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            GridViewExport.Export("Fev_16.xls", this.GridViewQuantidades);
        }

        protected void GridViewQuantidades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (!e.Row.RowIndex.Equals(-1))
            {
                int pdfOk = Convert.ToInt32(e.Row.Cells[10].Text);
                int idGrupo = Convert.ToInt32(e.Row.Cells[9].Text);
                string mesReferencia = e.Row.Cells[1].Text;
                int quantidade = Convert.ToInt32(e.Row.Cells[2].Text);
                decimal percentual = Convert.ToDecimal(e.Row.Cells[3].Text);
                int quantidadeMais = Convert.ToInt32(e.Row.Cells[4].Text);
                decimal percentualMais = Convert.ToDecimal(e.Row.Cells[5].Text);
                decimal valorConteudo = Convert.ToDecimal(e.Row.Cells[6].Text);
                decimal valorMaisAcessados = Convert.ToDecimal(e.Row.Cells[7].Text);
                decimal valorTotalRepasse = Convert.ToDecimal(e.Row.Cells[8].Text);
                

                _quantidadeTotal = _quantidadeTotal + quantidade;
                _percentualTotal = _percentualTotal + percentual;
                _quantidadeMaisTotal = _quantidadeMaisTotal + quantidadeMais;
                _valorPorQuantidade = _valorPorQuantidade + valorConteudo;
                _valorPorAcesso = _valorPorAcesso + valorMaisAcessados;
                _valorTotalRepasse = _valorTotalRepasse + valorTotalRepasse;
                _percentualReferenciaMaisAcessado = _percentualReferenciaMaisAcessado + percentualMais;

            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if ( Convert.ToInt16(Session["_classificacao"]) == 1)
                {
                    local = "en-US";
                }
                e.Row.Cells[2].Text = _quantidadeTotal.ToString();
                e.Row.Cells[3].Text = Math.Round(_percentualTotal, 2).ToString();
                e.Row.Cells[4].Text = _quantidadeMaisTotal.ToString();
                e.Row.Cells[5].Text = Math.Round(_percentualReferenciaMaisAcessado, 2).ToString();
                e.Row.Cells[6].Text = _valorPorQuantidade.ToString("C2", CultureInfo.CreateSpecificCulture(local));
                e.Row.Cells[7].Text = _valorPorAcesso.ToString("C2", CultureInfo.CreateSpecificCulture(local));
                e.Row.Cells[8].Text = _valorTotalRepasse.ToString("C2", CultureInfo.CreateSpecificCulture(local));

            }
        }
    }
}
