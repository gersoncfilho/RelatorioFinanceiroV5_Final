using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelatorioFinanceiroV5.Models
{
    public class PDFGrupo
    {
        public int IdGrupo { get; set; }
        public string MesReferencia { get; set; }
        public string Grupo { get; set; }
        public int Quantidade { get; set; }
        public decimal  Percentual { get; set; }
        public int QuantidadeMaisAcessados { get; set; }
        public decimal PercentualMaisAcessados { get; set; }
        public decimal ValorConteudo { get; set; }
        public decimal ValorMaisAcessados { get; set; }
        public decimal ValorTotalRepasse { get; set; }

    }
}