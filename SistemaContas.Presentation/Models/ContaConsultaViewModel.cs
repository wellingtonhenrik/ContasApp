using ContasApp.Data.Entities;
using ContasApp.Data.Enums;

namespace ContasApp.Presentation.Models
{
    public class ContaConsultaViewModel
    {
        public ContaConsultaViewModel() 
        {
            Resultado = new List<ContasConsultaResultadoViewModel>();
        }

        public Guid? ContaId { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public List<ContasConsultaResultadoViewModel>? Resultado { get; set; }  
    }
}
