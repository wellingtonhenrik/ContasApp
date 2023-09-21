using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    public class AccountRegisterViewModel
    {
        [Required(ErrorMessage = "Informe seu nome")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Informe seu enderoço de email")]
        [EmailAddress(ErrorMessage = ("Informe um email válido"))]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [MinLength(6, ErrorMessage = "Senha deve conter mínimo {1} caracteres")]
        [MaxLength(20, ErrorMessage = "Senha deve conter máximo {1} caracteres")]
        [RegularExpression("(?=^.{8,}$)((?=.*\\d)(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$",ErrorMessage ="Seha não atende os requesitos")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas informadas não conferem")]
        [Required(ErrorMessage = "Informe sua senha de confirmação")]
        public string? ConfirmaSenha { get; set; }
    }
}
