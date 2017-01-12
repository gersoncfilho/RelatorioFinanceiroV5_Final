using MySql.Data.MySqlClient;
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
    public partial class RelatorioAcesso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var myConn = Classes.Connection.conn();

            if (!this.IsPostBack)
            {
                ddlMesReferencia.DataSource = Services.getMesReferencia(myConn);
                ddlMesReferencia.DataBind();

                Session["_mesReferencia"] = "Jul.16";

                BindGrid(myConn);
                pnlBodyOld.Visible = true;
                myConn.Close();
            }
        }

        private void BindGrid(MySqlConnection conn)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            using (conn)
            {
                
                dt = Services.GetGrupos(conn);
                GridViewQuantidades.DataSource = dt;
                GridViewQuantidades.DataBind();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            var _mesReferencia = ddlMesReferencia.SelectedValue;
            Debug.WriteLine(_mesReferencia.ToString());
            Session["_mesReferencia"] = _mesReferencia;
        }

        protected void GridViewQuantidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Visualizar")
            {
                var myConn = Connection.conn();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewQuantidades.Rows[index];
                int idGrupo = Convert.ToInt32(row.Cells[0].Text);

                //lblMesReferencia.Text = row.Cells[3].Text;

                lblGrupo.Text = Services.GetNomeGrupo(idGrupo);
               

                DataTable acessoGrupo = new DataTable();

                acessoGrupo = Services.GetAcessos(myConn, idGrupo);

                lblMesReferencia.Text = acessoGrupo.Rows[0].Field<string>("mesReferencia");

                GridViewAcessos.DataSource = acessoGrupo;
                GridViewAcessos.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "openModal();", true);
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {

        }

        protected void GridViewAcessos_DataBound(object sender, EventArgs e)
        {
            
        }
    }
}