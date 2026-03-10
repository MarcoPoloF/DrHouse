using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ParaFiltroUsuarioConsultorio : ParaFiltroUsuario
    {
        public int? IdConsultorio { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
