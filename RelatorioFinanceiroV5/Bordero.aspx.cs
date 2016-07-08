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
    public partial class Bordero : System.Web.UI.Page
    {
        int _quantTotal = 0;
        int _total = 0;
        int _quantidade = 0;
        decimal _percentual = 0.0m;
        decimal _percentualTotal = 0.0m;
        private const int round = 6;
        private string _mesReferencia = "Jan_16";

        protected void Page_Load(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            _quantTotal = Service.QuantidadeTotal(myConn, _mesReferencia);

            if (!this.IsPostBack)
            {
                pnlBordero.Visible = false;
                ddlMesReferencia.DataSource = Service.getMesReferencia(myConn);
                ddlMesReferencia.DataBind();
                myConn.Close();
                BindGrid(_mesReferencia, myConn);
                
            }
        }

        private void BindGrid(string mesReferencia, MySqlConnection myConn)
        {
            DataTable dt = new DataTable();
            dt = Service.GetValoresBordero(myConn, mesReferencia);
            grdBordero.DataSource = dt;
            grdBordero.DataBind();
            myConn.Close();
            pnlBordero.Visible = true;
        }

        
        protected void ddlMesReferencia_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(ddlMesReferencia.SelectedItem.ToString());
            var myConn = Connection.conn();
            //DataTable dt = new DataTable();
            //dt = Service.GetValoresBordero(myConn, ddlMesReferencia.SelectedItem.ToString());
            //grdBordero.DataSource = dt;
            //grdBordero.DataBind();
            //myConn.Close();
            //pnlBordero.Visible = true;
            BindGrid(ddlMesReferencia.SelectedItem.ToString(), myConn);

        }

        protected void grdBordero_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Debug.WriteLine(e.Row.Cells[1].Text);
                _quantidade = Convert.ToInt32(e.Row.Cells[1].Text);
                _total = _total + _quantidade;
                _percentual = Math.Round(_quantidade / (decimal)_quantTotal * 100, round);
                _percentualTotal = _percentualTotal + _percentual;

                e.Row.Cells[2].Text = Convert.ToString(_percentual);


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = Convert.ToString(_total);
                e.Row.Cells[2].Text = Convert.ToString(_percentualTotal);
            }
            
        }
    }
}