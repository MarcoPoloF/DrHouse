using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class TomaMedicamento
    {
        public string IdUsuario { get; set; }
        public string IdUsuarioAdicional { get; set; }
        public int IdMedicamento { get; set; }
        public DateTime FechaToma { get; set; }
    }
}
