using RelatorioFinanceiroV5.Domain.Entities;
using RelatorioFinanceiroV5.SharedKernel.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.Domain.Scopes
{
    public static class UsuarioScopes
    {
        public static bool RegisterUsuarioScopeIsValid(this Usuario usuario)
        {
            return AssertionConcern.IsSatisfiedBy
                (
                    AssertionConcern.AssertEmailIsValid(usuario.Email, "Email inválido"),
                    AssertionConcern.AssertNotEmpty(usuario.Senha, "Senha obrigatória")
                );
        }

        public static bool AutenticaUsuarioScopeIsValid(this Usuario usuario, string email, string senha)
        {
            return AssertionConcern.IsSatisfiedBy
                (
                    AssertionConcern.AssertNotEmpty(email, "Usuário é obrigatório"),
                    AssertionConcern.AssertNotEmpty(senha, "Senha é obrigatória"),
                    AssertionConcern.AssertAreEquals(usuario.Email, email, "Usuário ou senha inválidos"),
                    AssertionConcern.AssertAreEquals(usuario.Senha, senha, "Usuário ou senha inválidos")
                );
        }
    }
}
