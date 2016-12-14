using RelatorioFinanceiroV5.Classes;
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
    public partial class Bordero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!this.IsPostBack)
            {
                pnlBordero.Visible = false;
                var myConn = Connection.conn();
                ddlMesReferencia.DataSource = Service.getMesReferencia(myConn);
                ddlMesReferencia.DataBind();
                string[] classificacao = { "Nacional", "Internacional" };
                ddlClassificacao.DataSource = classificacao;
                ddlClassificacao.DataBind();
                myConn.Close();
                //BindGrid("Jan_16", myConn);
            }
        }

       
        protected void grdBordero_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Debug.WriteLine(e.Row.Cells[1].Text);
               
                
            }
        }

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            GridViewExport.Export("bordero.xls", grdBordero);
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(ddlMesReferencia.SelectedItem.ToString());
            var myConn = Connection.conn();
            DataTable dt = new DataTable();
            dt = Service.GetValoresBordero(myConn, ddlMesReferencia.SelectedItem.ToString(), ddlClassificacao.SelectedItem.ToString());
            grdBordero.DataSource = dt;
            grdBordero.DataBind();
            myConn.Close();
            pnlBordero.Visible = true;
        }
    }
}