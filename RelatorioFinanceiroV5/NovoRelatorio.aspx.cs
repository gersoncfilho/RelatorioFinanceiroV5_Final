using MySql.Data.MySqlClient;
using RelatorioFinanceiroV5.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RelatorioFinanceiroV5
{


    public partial class NovoRelatorio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string[] _mes_referencia = { "Jan_16", "Fev_16", "Mar_16", "Abr_16", "Mai_16", "Jun_16", "Jul_16", "Ago_16", "Set_16", "Out_16", "Nov_16", "Dez_16" };
                ddlMes.DataSource = _mes_referencia;
                ddlMes.DataBind();
            }
        }

        #region metodos
        //verifica se o mes solicitado ja foi gravado no banco
        private bool RelatorioExistente(string _mes_referencia)
        {
            var myConn = Connection.conn();
            try
            {
                myConn.Open();
                var query = string.Format("select relatorio_gerado from mes_referencia where mes = '{0}'", _mes_referencia);
                MySqlCommand cmd = new MySqlCommand(query, myConn);

                int result = Convert.ToInt32(cmd.ExecuteScalar());

                if (result > 0)
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
                return false;
            }
        }

        private bool VerificaTabelas(string _mes_referencia, string _nomeTabela)
        {
            var myConn = Connection.conn();
            try
            {
                myConn.Open();
                var query = string.Format("select count(mes_referencia) from {1} where mes_referencia = '{0}'", _mes_referencia, _nomeTabela);
                MySqlCommand cmd = new MySqlCommand(query, myConn);

                int result = Convert.ToInt32(cmd.ExecuteScalar());
                myConn.Close();

                if (result > 0)
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

        private bool VerificaAjusteQuantidades(string _mes_referencia)
        {
            var myConn = Connection.conn();
            try
            {
                myConn.Open();
                var query = string.Format("select count(ajuste_quantidades) from quantidades where ajuste_quantidades = 1 and mes_referencia = '{0}'", _mes_referencia);
                MySqlCommand cmd = new MySqlCommand(query, myConn);

                int result = Convert.ToInt32(cmd.ExecuteScalar());
                myConn.Close();

                if (result > 0)
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

        private bool VerificaRefxMais(string _mes_referencia)
        {
            var myConn = Connection.conn();
            try
            {
                myConn.Open();
                var query = string.Format("select count(comparado_com_referencia) from mais_acessados_por_editora where comparado_com_referencia = 1 and mes_referencia = '{0}'", _mes_referencia);
                MySqlCommand cmd = new MySqlCommand(query, myConn);

                int result = Convert.ToInt32(cmd.ExecuteScalar());
                myConn.Close();

                if (result > 0)
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


        #endregion

        #region botoes
        //Botao para selecionar o mes a ser gerado
        protected void btnOK_Click(object sender, EventArgs e)
        {
            PanelAcoes.Visible = false;
            PanelInfo.Visible = false;
            string _mesSelecionado = ddlMes.SelectedValue.ToString();
            if (VerificaTabelas(_mesSelecionado,"bordero"))
            {
                PanelInfo.Visible = true;
                Debug.WriteLine(_mesSelecionado + " já foi gerado");
            }
            else
            {
                Debug.WriteLine("Relatório inexistente");
                PanelAcoes.Visible = true;
                if (VerificaTabelas(_mesSelecionado, "quantidades"))
                {
                    PanelAlertQuantidades.Visible = true;
                    PanelAlertQuantidades.CssClass = "alert alert-success";
                    btnAppend.CssClass = "btn btn-success btn-xs";
                    iconQuantidades.Visible = true;
                    btnAppend.Enabled = false;
                }
                else
                {
                    PanelAlertQuantidades.Visible = true;
                }

                if (VerificaTabelas(_mesSelecionado, "mais_acessados_por_editora"))
                {
                    PanelAlertMaisAcessados.Visible = true;
                    PanelAlertMaisAcessados.CssClass = "alert alert-success";
                    btnAppendMaisAcessados.CssClass = "btn btn-success btn-xs";
                    iconMaisAcessados.Visible = true;
                    btnAppendMaisAcessados.Enabled = false;
                }
                else
                {
                    PanelAlertMaisAcessados.Visible = true;
                }

                if (VerificaAjusteQuantidades(ddlMes.SelectedValue.ToString()))
                {
                    PanelAjusteQuantidades.Visible = true;
                    PanelAjusteQuantidades.CssClass = "alert alert-success";
                    btnAjustaQuantidades.CssClass = "btn btn-success btn-xs";
                    iconAjusteQuantidade.Visible = true;
                    btnAjustaQuantidades.Enabled = false;
                }
                else
                {
                    PanelAjusteQuantidades.Visible = true;
                }

                if (VerificaRefxMais(ddlMes.SelectedValue.ToString()))
                {
                    PanelRefxMais.Visible = true;
                    PanelRefxMais.CssClass = "alert alert-success";
                    btnRefxMais.CssClass = "btn btn-success btn-xs";
                    iconRefxMais.Visible = true;
                    btnRefxMais.Enabled = false;
                }
                else
                {
                    PanelRefxMais.Visible = true;
                }
                if (VerificaTabelas(ddlMes.SelectedValue.ToString(), "bordero"))
                {
                    PanelRefxMais.Visible = true;
                    PanelRefxMais.CssClass = "alert alert-success";
                    btnRefxMais.CssClass = "btn btn-success btn-xs";
                    iconRefxMais.Visible = true;
                    btnRefxMais.Enabled = false;
                }
                else
                {
                    PanelRefxMais.Visible = true;
                }

            }
        }



        protected void btnOkInfo_Click(object sender, EventArgs e)
        {
            PanelInfo.Visible = false;
            btnOK.Enabled = true;
        }

        //Botao para dar append na tabela quantidades
        protected void btnAppend_Click(object sender, EventArgs e)
        {

            var myConn = Connection.conn();

            try
            {
                myConn.Open();
                string procedureName = "01_appendQuantidades";

                MySqlCommand cmd = new MySqlCommand(procedureName, myConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_mesReferencia", ddlMes.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Falha no banco:" + ex.Message);
            }
            finally
            {
                myConn.Close();
            }
            myConn.Close();

            PanelAlertQuantidades.CssClass = "alert alert-success";
            iconQuantidades.Visible = true;
            btnAppend.CssClass = "btn btn-success btn-xs";
            btnAppend.Enabled = false;
        }

        protected void btnAppendMaisAcessados_Click(object sender, EventArgs e)
        {
            var myConn = Connection.conn();

            try
            {
                myConn.Open();
                string procedureName = "02_appendMaisAcessados";

                MySqlCommand cmd = new MySqlCommand(procedureName, myConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_mesReferencia", ddlMes.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Falha no banco:" + ex.Message);
            }
            finally
            {
                myConn.Close();
            }
            myConn.Close();

            PanelAlertMaisAcessados.CssClass = "alert alert-success";
            iconMaisAcessados.Visible = true;
            btnAppendMaisAcessados.CssClass = "btn btn-success btn-xs";
            btnAppendMaisAcessados.Enabled = false;
        }

        protected void btnAjustaQuantidades_Click(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            bool success = true;
            try
            {
                myConn.Open();
                string procedureName = "03_ajusteQuantidades";

                MySqlCommand cmd = new MySqlCommand(procedureName, myConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_mesReferencia", ddlMes.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Falha no banco:" + ex.Message);
                success = false;
            }
            finally
            {
                myConn.Close();

            }
            myConn.Close();

            if (success == false)
            {
                PanelAjusteQuantidades.CssClass = "alert alert-danger";
                btnAjustaQuantidades.CssClass = "btn btn-danger btn-xs";
                btnAjustaQuantidades.Enabled = false;
                lblAjustaQuantidades.Text = "Erro de acesso ao banco MySQL!";

            }
            else
            {
                PanelAjusteQuantidades.CssClass = "alert alert-success";
                iconAjusteQuantidade.Visible = true;
                btnAjustaQuantidades.CssClass = "btn btn-success btn-xs";
                btnAjustaQuantidades.Enabled = false;
            }

        }

        protected void btnRefxMais_Click(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            bool success = true;
            try
            {
                myConn.Open();
                string procedureName = "04_executaRefMaisAcessados";

                MySqlCommand cmd = new MySqlCommand(procedureName, myConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_mesReferencia", ddlMes.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Falha no banco:" + ex.Message);
                success = false;
            }
            finally
            {
                myConn.Close();

            }
            myConn.Close();

            if (success == false)
            {
                PanelRefxMais.CssClass = "alert alert-danger";
                btnRefxMais.CssClass = "btn btn-danger btn-xs";
                btnRefxMais.Enabled = false;
                lblRefxMais.Text = "Erro de acesso ao banco MySQL!";

            }
            else
            {
                PanelRefxMais.CssClass = "alert alert-success";
                iconRefxMais.Visible = true;
                btnRefxMais.CssClass = "btn btn-success btn-xs";
                btnRefxMais.Enabled = false;
            }
        }

       

        protected void btnGeraBordero_Click(object sender, EventArgs e)
        {
            var myConn = Connection.conn();
            bool success = true;
            try
            {
                myConn.Open();
                string procedureName = "05_geraBordero";

                MySqlCommand cmd = new MySqlCommand(procedureName, myConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_mesReferencia", ddlMes.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Falha no banco:" + ex.Message);
                success = false;
            }
            finally
            {
                myConn.Close();

            }
            myConn.Close();

            if (success == false)
            {
                PanelGeraBordero.CssClass = "alert alert-danger";
                btnGeraBordero.CssClass = "btn btn-danger btn-xs";
                btnGeraBordero.Enabled = false;
                lblGeraBordero.Text = "Erro de acesso ao banco MySQL!";

            }
            else
            {
                PanelGeraBordero.CssClass = "alert alert-success";
                iconGeraBordero.Visible = true;
                btnGeraBordero.CssClass = "btn btn-success btn-xs";
                btnGeraBordero.Enabled = false;
            }
        }

        #endregion


    }
}