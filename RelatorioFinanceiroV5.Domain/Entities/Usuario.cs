using RelatorioFinanceiroV5.Domain.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int PerfilId { get; set; }

        public Usuario(string email, string senha, int perfilId)
        {
            this.Email = email;
            this.Senha = senha;
            this.PerfilId = perfilId;
        }

        public void Register()
        {
            this.RegisterUsuarioScopeIsValid();
        }

        public void Autentica(string email, string senha)
        {
            this.AutenticaUsuarioScopeIsValid(email, senha);
        }

        public void MudaPerfil(int novoPerfil)
        {
            this.PerfilId = novoPerfil;
        }



    }
}
