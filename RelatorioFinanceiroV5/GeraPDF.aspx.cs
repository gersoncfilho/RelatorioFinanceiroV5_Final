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
        protected void Page_Load(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            string[] classificacao = { "Nuvem de Livros", "Nube de Libros" };


            if (!this.IsPostBack)
            {
                ddlClassificacao.DataSource = classificacao;
                ddlClassificacao.DataBind();

                ddlMesReferencia.DataSource = Service.getMesReferencia(myConn);
                ddlMesReferencia.DataBind();
                pnlBodyOld.Visible = true;
                myConn.Close();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            dynamic _quantidade;
            Session["_classificacao"] = ddlClassificacao.SelectedIndex +1;
            Session["_mesReferencia"] = ddlMesReferencia.SelectedValue;
            DataTable editoras = new DataTable();
            editoras = Service.GetValoresBordero(myConn,ddlMesReferencia.SelectedValue, (Convert.ToInt16(Session["_classificacao"])));

            if (editoras != null)
            {
                foreach (DataRow row in editoras.Rows)
                {
                    //_quantidade = row.ItemArray("Quantidade");
                    PDFGrupo editora = new PDFGrupo();
                    editora.Grupo = row.ItemArray[0].ToString();
                    editora.MesReferencia = Session["_mesReferencia"].ToString();
                    editora.Quantidade = Convert.ToInt32(row.ItemArray[2].ToString());
                    editora.Percentual = Convert.ToDecimal(row.ItemArray[3].ToString());
                    editora.QuantidadeMaisAcessados = Convert.ToInt32(row.ItemArray[4].ToString());
                    editora.PercentualMaisAcessados = Convert.ToDecimal(row.ItemArray[5].ToString());
                    editora.ValorConteudo = Convert.ToDecimal(row.ItemArray[6].ToString());
                    editora.ValorMaisAcessados = Convert.ToDecimal(row.ItemArray[7].ToString());
                    editora.ValorTotalRepasse = Convert.ToDecimal(row.ItemArray[8].ToString());

                    Debug.WriteLine(editora);

                    PDFSharpHelper.CreatePDF(editora, Convert.ToInt16(Session["_classificacao"]));

                }
            }
            else
            {

            }


        }
    }
}