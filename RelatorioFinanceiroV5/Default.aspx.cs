using MySql.Data.MySqlClient;
using RelatorioFinanceiroV5.Classes;
using RelatorioFinanceiroV5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RelatorioFinanceiroV5
{
    public partial class Default : System.Web.UI.Page
    {
       

        [WebMethod]
        public static List<ReceitaChartDetails> GetChartData()
        {
            var myConn = Connection.conn();
            DataTable dt = new DataTable();
            dt = Service.GetReceitaGrafico(myConn);
            List<ReceitaChartDetails> dataList = new List<ReceitaChartDetails>();
            foreach (DataRow dtRow in dt.Rows)
            {
                ReceitaChartDetails details = new ReceitaChartDetails();
                details.ReceitaMensal = Convert.ToDecimal(dtRow[0]);
                details.Mes = dtRow[1].ToString();
                dataList.Add(details);
            }
            return dataList;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
        

        }


    }
}