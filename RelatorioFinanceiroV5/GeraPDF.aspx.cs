using RelatorioFinanceiroV5.Classes;
using RelatorioFinanceiroV5.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RelatorioFinanceiroV5
{
    public partial class GeraPDF : System.Web.UI.Page
    {
        int _idMoeda;
        int _classificacao;

        protected void Page_Load(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            string[] classificacao = { "Nuvem de Livros", "Nube de Libros" };
            


            if (!this.IsPostBack)
            {
                ddlClassificacao.DataSource = classificacao;
                ddlClassificacao.DataBind();

                ddlMesReferencia.DataSource = Services.getMesReferencia(myConn);
                ddlMesReferencia.DataBind();
                pnlBodyOld.Visible = true;
                myConn.Close();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            //dynamic _quantidade;


            if (ddlClassificacao.SelectedIndex == 0)
            {
                _idMoeda = 1;
                _classificacao = 0;
            }
            else
            {
                _idMoeda = 2;
                _classificacao = 1;
            }

            Session["_mesReferencia"] = ddlMesReferencia.SelectedValue;
            DataTable editoras = new DataTable();
            //editoras = Services.GetValoresBordero(myConn,ddlMesReferencia.SelectedValue, (Convert.ToInt16(Session["_classificacao"])));

            editoras = Services.getQuantidadeConteudoPorGrupo(ddlMesReferencia.SelectedValue, _classificacao, myConn);

            if (editoras != null)
            {
                foreach (DataRow row in editoras.Rows)
                {
                    //_quantidade = row.ItemArray("Quantidade");
                    PDFGrupo editora = new PDFGrupo();
                    editora.Grupo = row.ItemArray[2].ToString();
                    editora.MesReferencia = Session["_mesReferencia"].ToString();
                    editora.Quantidade = Convert.ToInt32(row.ItemArray[3].ToString());
                    editora.Percentual = Convert.ToDecimal(row.ItemArray[4].ToString());
                    editora.QuantidadeMaisAcessados = Convert.ToInt32(row.ItemArray[5].ToString());
                    editora.PercentualMaisAcessados = Convert.ToDecimal(row.ItemArray[6].ToString());
                    editora.ValorConteudo = Convert.ToDecimal(row.ItemArray[7].ToString());
                    editora.ValorMaisAcessados = Convert.ToDecimal(row.ItemArray[8].ToString());
                    editora.ValorTotalRepasse = Convert.ToDecimal(row.ItemArray[9].ToString());

                    Debug.WriteLine(editora);

                    PDFSharpHelper.CreatePDF(editora, _idMoeda);

                }
            }
            else
            {

            }


        }
    }
}