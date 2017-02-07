using RelatorioFinanceiroV5.Domain.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.Domain.Entities
{
    public class Editora
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        public bool Internacional { get; private set; }
        public int GrupoId { get; private set; }
        public int ClassificacaoId { get; private set; }

        public Editora(string nome, bool ativo, bool Internacional)
        {
            this.Nome = nome;
            this.Ativo = ativo;
            this.Internacional = Internacional;
        }

        public void Register()
        {
            this.RegisterEditoraScopeIsValid();
        }

        public void EstaAtiva()
        {
            this.Ativo = true;
        }

        public void EstaInativa()
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
