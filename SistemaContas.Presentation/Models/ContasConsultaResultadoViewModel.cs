using ContasApp.Data.Entities;
using ContasApp.Data.Enums;

namespace ContasApp.Presentation.Models
{
    /// <summary>
    /// Modelo de dadaos para o resultado da consulta de contas
    /// </summary>
    public class ContasConsultaResultadoViewModel
    {
        public Guid? ContaId { get; set; }  
        public string? Nome { get; set; }
        public DateTime? Data { get; set; } 
        public string? Categoria { get; set; }    
        public string? Tipo { get; set; } 
        public decimal? Valor { get; set; }
        public string? Observacao { get; set; }  
        public string? StatusConta { get; set; }    
    }
}
