using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Messages.Models
{
    public class EmailMessagemModel
    {
        public string? EmailDestinatario { get; set; }
        public string? Assunto { get; set; }
        public string? Corpo { get; set; }
    }
}
