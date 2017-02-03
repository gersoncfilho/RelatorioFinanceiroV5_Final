using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RelatorioFinanceiroV5.Classes;
using System.Diagnostics;

namespace RelatorioFinanceiroV5
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.Abandon();
            }
            if (Request.QueryString["info"] != null)
            {
                string message = Request.QueryString["info"].ToString();
                if (message == "0")
                {
                    Response.Write("<strong>Você precisa logar primeiro.");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!txtUsuario.Equals("") && !txtPassword.Equals(""))
            {
                var usuario = Services.ValidaUsuario(txtUsuario.Text, txtPassword.Text);

                if (usuario == null)
                {
                    lblMensagem.Text = "Usuário não encontrado.";
                    txtPassword.Text = string.Empty;
                    txtUsuario.Text = string.Empty;
                    return;
                }
                else
                {
                    Session["username"] = txtUsuario.Text.Trim();
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                lblMensagem.Text = "Usuario e senha sao obrigatorios.";
            }
            
        }
    }
}