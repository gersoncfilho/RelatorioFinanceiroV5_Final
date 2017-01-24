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
using RelatorioFinanceiroV5.Classes.Service;

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
        private const int _moeda = 2; //1-Real, 2-US$, 3-Euro
        //private int _classificacao;
        private List<Grupo> grupos = new List<Grupo>();

        int _idMoeda = 1;
        int _classificacao = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            _quantidadeTotal = Services.QuantidadeTotal(myConn, "Abr_16");
            string[] classificacao = { "Nuvem de Livros", "Nube de Libros" };
            Debug.WriteLine("Quantidade total: " + _quantidadeTotal);

            if (!this.IsPostBack)
            {
                ddlClassificacao.DataSource = classificacao;
                ddlClassificacao.DataBind();


                ddlMesReferencia.DataSource = Services.getMesReferencia(myConn);
                //ddlMesReferencia.DataSource = DbService.LoadMesReferencia();
                ddlMesReferencia.DataBind();
                ddlMesReferencia.SelectedIndex = 4;


                BindGrid("Mai_16", 0, myConn);
                pnlBodyOld.Visible = true;
                myConn.Close();
            }
        }

        private void BindGrid(string mesReferencia, int classificacao, MySqlConnection conn)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            using (conn)
            {
                dt = Services.getQuantidadeConteudoPorGrupo(mesReferencia, classificacao, conn);
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

                decimal receita = Services.GetReceita(myConn, row.Cells[1].Text, _moeda);
                if (Convert.ToInt16(Session["_classificacao"]) > 0)
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


                decimal _repasseQuant = Convert.ToDecimal(row.Cells[6].Text);
                lblValorRepasseQuantidade.Text = _repasseQuant.ToString(); //_repasseQuant.ToString("C2", CultureInfo.CreateSpecificCulture(local));

                decimal _valorRepMaisAce = Convert.ToDecimal(row.Cells[7].Text);
                lblValorRepasseRefMaisAcessados.Text = _valorRepMaisAce.ToString(); //_valorRepMaisAce.ToString("C2", CultureInfo.CreateSpecificCulture(local));

                decimal _valorTotalRepasse = Convert.ToDecimal(row.Cells[8].Text);
                lblValorTotalRepasse.Text = _valorTotalRepasse.ToString();//_valorTotalRepasse.ToString("C2", CultureInfo.CreateSpecificCulture(local));

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "openModal();", true);
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Click here");
            PDFGrupo editora = new PDFGrupo();

            editora.Grupo = lblGrupo.Text;
            editora.MesReferencia = lblMes.Text;
            editora.Quantidade = Convert.ToInt32(lblQuantidadeConteudos.Text);
            editora.Percentual = Convert.ToDecimal(lblPercentualEditoraTotal.Text);
            editora.QuantidadeMaisAcessados = Convert.ToInt32(lblTotalRefMaisAcessados.Text);
            editora.PercentualMaisAcessados = Convert.ToDecimal(lblPercentual10MaisAcessados.Text);
            editora.ValorConteudo = Convert.ToDecimal(lblValorRepasseQuantidade.Text);
            editora.ValorMaisAcessados = Convert.ToDecimal(lblValorRepasseRefMaisAcessados.Text);
            editora.ValorTotalRepasse = Convert.ToDecimal(lblValorTotalRepasse.Text);

            Debug.WriteLine(editora);

            PDFSharpHelper.CreatePDF(editora, _idMoeda);

           

        }

        protected void btnOK_OnClick(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            var _mes_referencia = ddlMesReferencia.SelectedValue;
            Session["_classificacao"] = ddlClassificacao.SelectedIndex;
            Session["_mesReferencia"] = ddlMesReferencia.SelectedItem;

            if (Convert.ToInt16(Session["_classificacao"]) == 0)
            {
                _idMoeda = 1;
                _classificacao = 1;
            }
            else
            {
                _idMoeda = 2;
                _classificacao = 2;
            }



            Debug.WriteLine(_mes_referencia);
            BindGrid(_mes_referencia, Convert.ToInt16(Session["_classificacao"]), myConn);
            _totalPercentual = Services.QuantidadeTotal(myConn, _mes_referencia);
        }


        #region Methods
        private decimal getPercentualPorQuantidade(MySqlConnection myConn, string mes_referencia, int quantidade)
        {
            int quantidadeTotal = Services.QuantidadeTotal(myConn, mes_referencia);
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
                if (Convert.ToInt16(Session["_classificacao"]) == 2)
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
