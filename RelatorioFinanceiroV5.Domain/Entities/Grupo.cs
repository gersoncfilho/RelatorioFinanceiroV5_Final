using RelatorioFinanceiroV5.Domain.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.Domain.Entities
{
    public class Grupo
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        public bool Internacional { get; private set; }
        public int ClassificacaoId { get; set; }

        public Grupo(string nome, bool ativo, bool internacional)
        {
            this.Nome = nome;
            this.Ativo = ativo;
            this.Internacional = internacional;
        }

        private void Register()
        {
            this.RegisterGrupoScopeIsValid();
        }

        private void EstaAtivo()
        {
            this.Ativo = true;
        }

        private void EstaInativo()
        {
            this.Ativo = false;
        }
        public void EInternacional()
        {
            this.Internacional = true;
        }

        public void ENacional()
        {
            this.Internacional = false;
        }
    }
}
