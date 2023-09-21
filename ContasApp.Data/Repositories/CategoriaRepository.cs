using ContasApp.Data.Contexts;
using ContasApp.Data.Entities;
using ContasApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>
    {
        public Categoria? GetByNomeUserTipoId(string nome, Guid? userId, TipoCategoria? tipo, Guid? id)
        {
            using (var contexto = new DataContext())
            {
                return contexto.Categoria?.FirstOrDefault(a => a.Nome.Equals(nome) && a.UsuarioId == userId && a.Tipo == tipo && a.CategoriaId != id);
            }
        }
        public List<Categoria> GetByUser(Guid? userId)
        {
            using (var contexto = new DataContext())
            {
                return contexto.Categoria?.Where(a=>a.UsuarioId == userId).OrderBy(a=>a.Nome).ToList();
            }
        }
    }
}
