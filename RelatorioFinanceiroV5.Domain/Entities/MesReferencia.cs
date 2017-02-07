using RelatorioFinanceiroV5.Domain.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.Domain.Entities
{
    public class MesReferencia
    {
        public int Id { get; private set; }
        public string Mes { get; private set; }

        public MesReferencia(string mes, bool foiGerado)
        {
            this.Mes = mes;
        }

        public void Register()
        {
            this.RegisterMesScopeIsValid();
        }     
    }
}
