using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Entities
{
    public class Conta
    {
        #region Propriedades
        public Guid? ContaId { get; set; }
        public string? Nome { get; set; }
        public DateTime? Data { get; set; }
        public decimal? Valor { get; set; }
        public string? Observacao { get; set; }
        public Guid? UsuarioId { get; set; }
        public Guid? CategoriaId { get; set; }
        #endregion

        #region Relacionamentos
        public Categoria? Categoria { get; set; }
        public Usuario? Usuario { get; set; }   
        #endregion
    }
}
