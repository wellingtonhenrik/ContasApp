using ContasApp.Data.Contexts;
using ContasApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>
    {
        public Usuario? GetByEmail(string email)
        {
            using (var context = new DataContext())
            {
                return context.Usuario?.FirstOrDefault(a => a.Email.Equals(email));
            }
        }    
        
        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            using (var context = new DataContext())
            {
                return context.Usuario?.FirstOrDefault(a => a.Email.Equals(email) && a.Senha.Equals(senha));
            }
        }
    }
}
