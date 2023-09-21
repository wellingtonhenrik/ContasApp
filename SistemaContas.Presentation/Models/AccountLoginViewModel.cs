using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    public class AccountLoginViewModel
    {
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido")]
        [Required(ErrorMessage = "Informe seu endereço de email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Informe sua senha de acesso")]
        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres")]
        [MaxLength(20, ErrorMessage = "Informe no máximo {1 } caracteres")]
        public string? Senha { get; set; }
    }
}
