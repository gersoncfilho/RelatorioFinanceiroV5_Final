using RelatorioFinanceiroV5.Domain.Entities;
using RelatorioFinanceiroV5.SharedKernel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.Domain.Scopes
{
    public static class MesScopes
    {
        public static bool RegisterMesScopeIsValid(this MesReferencia mesReferencia)
        {
            return AssertionConcern.IsSatisfiedBy
                (
                    AssertionConcern.AssertNotEmpty(mesReferencia.Mes, "Preenchimento do mês obrigatório");
                );
        }
            
    }
}
