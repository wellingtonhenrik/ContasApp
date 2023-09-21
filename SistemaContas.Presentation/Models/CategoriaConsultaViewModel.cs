using ContasApp.Data.Enums;

namespace ContasApp.Presentation.Models
{
    public class CategoriaConsultaViewModel
    {
        public string? Nome { get; set; }
        public TipoCategoria? Tipo { get; set; }
        public Guid? CategoriaId { get; set; }
    }
}
