using ContasApp.Data.Entities;
using ContasApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class ContaEdicaoViewModel
    {
        public Guid? ContaId { get; set; }
        public string? Tipo { get; set; }
        public string? Categoria { get; set; }

        [Required(ErrorMessage = "Informe campo {0}")]
        [MaxLength(30, ErrorMessage = "Maxímo {1} caracteres")]
        [MinLength(2, ErrorMessage = "Minímo {1} caracteres")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Selecione uma Categoria")]
        public Guid? CategoriaId { get; set; }
        [Required(ErrorMessage = "Informe campo {0}")]
        public DateTime? Data { get; set; }

        public string? Observacao { get; set; }
        [Required(ErrorMessage = "Informe campo {0}")]
        public decimal? Valor { get; set; }
    }
}
