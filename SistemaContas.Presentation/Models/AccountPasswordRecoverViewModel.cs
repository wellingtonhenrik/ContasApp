using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    public class AccountPasswordRecoverViewModel
    {
        [Required(ErrorMessage = "Informe o seu email")]
        [EmailAddress(ErrorMessage = "Informe um email válido")]
        public string? Email { get; set; }
    }
}
