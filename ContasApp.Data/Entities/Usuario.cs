﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Entities
{
    public class Usuario
    {
        #region Propriedades
        public Guid? UsuarioId { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }

        public Guid? ContasId { get; set; }
        public Guid? CategoriasId { get; set; }  
        #endregion

        #region Relacionamentos
        public List<Conta>? Contas { get; set; }
        public List<Categoria>? Categorias { get; set; } 
        #endregion
    }
}
