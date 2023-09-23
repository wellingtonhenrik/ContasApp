using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Enums
{
    public enum StatusConta
    {
        [Description("Pendente")]
        Pendente = 0,
        [Description("Pagar")]
        Paga = 1,
        [Description("Paga Parcial")]
        PagaParcial = 2
    }
}
