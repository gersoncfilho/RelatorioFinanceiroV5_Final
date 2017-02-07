using RelatorioFinanceiroV5.Domain.Entities;
using RelatorioFinanceiroV5.SharedKernel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.Domain.Scopes
{
    public static class EditoraScopes
    {
        public static bool RegisterEditoraScopeIsValid(this Editora editora)
        {
            return AssertionConcern.IsSatisfiedBy
                (
                    AssertionConcern.AssertNotEmpty(editora.Nome, "Nome Obrigatório")
                );
        }
    }
}
