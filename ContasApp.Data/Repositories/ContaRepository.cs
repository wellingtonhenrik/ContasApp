using ContasApp.Data.Contexts;
using ContasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Repositories
{
    public class ContaRepository : BaseRepository<Conta>
    {
        public Conta GetByNomeIdCategoria(string nome, Guid? contaId, Guid? categoriaId, Guid? userId)
        {
            using (var context = new DataContext())
            {
                return context.Conta.FirstOrDefault(a => a.Nome.Equals(nome) && a.ContaId != contaId && a.CategoriaId == categoriaId && a.UsuarioId == userId);
            }
        }

        public List<Conta> GetByUserId(Guid? userId)
        {
            using (var context = new DataContext())
            {
                return context.Conta.Include(a => a.Categoria).Where(a => a.UsuarioId == userId).OrderBy(a => a.Data).ToList();
            }
        }

        public override Conta GetById(Guid? id)
        {
            using (var context = new DataContext()) 
            {
                return context.Conta.Include(a=>a.Categoria).FirstOrDefault(a => a.ContaId == id);
            }
        }
        public List<Conta> GetByUserIdAndDatas(Guid? userId, DateTime? dataInicio, DateTime? dataFim)
        {
            using (var context = new DataContext())
            {
                return context.Conta.Include(a => a.Categoria).Where(a => a.UsuarioId == userId && a.Data >= dataInicio && a.Data <= dataFim).OrderBy(a=>a.Data).ToList();
            }
        }
        public List<Conta> GetListGetNomeIdCategoria(string nome, DateTime? dataConta, Guid? categoriaId, Guid? userId)
        {
            using (var context = new DataContext())
            {
                return context.Conta.Where(a => a.Nome.Equals(nome) && a.Data >= dataConta && a.CategoriaId == categoriaId && a.UsuarioId == userId).ToList();
            }
        }
    }
}
