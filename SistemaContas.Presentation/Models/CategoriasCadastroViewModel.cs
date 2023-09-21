using ContasApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ContasApp.Presentation.Models
{
    public class CategoriasCadastroViewModel
    {
        [Required(ErrorMessage ="Informe {0}")]
        [MinLength(3, ErrorMessage = "Informe mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage ="Informe máximo {1} caracteres")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Informe {0}")]
        public TipoCategoria? Tipo { get; set; }
    }
}
