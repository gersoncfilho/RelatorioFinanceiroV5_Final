﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelatorioFinanceiroV5.Models
{
    public class Editora
    {

        public int IdEditora { get; set; }
        public string NomeEditora { get; set; }
        public string Mes_Referencia { get; set; }
        public int Quantidade { get; set; }
        public decimal Percentual_Quantidade { get; set; }
        public int Quant_Ref_Mais_Acessados { get; set; }
        public decimal Receita { get; set; }
        public decimal ReceitaASerDividida { get; set; }
        public decimal Receita10 { get; set; }
        public decimal Receita20 { get; set; }
        public decimal Percentual_Ref_Mais_Acessados { get; set; }
        public decimal Valor_Conteudo { get; set; }
        public decimal Valor_Mais_Acessados { get; set; }
        public decimal Valor_Repasse { get; set; }
    }
}