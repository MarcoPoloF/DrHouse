using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class UsuarioAntecedenteFamiliarPatologico
    {
        public string IdUsuario { get; set; }
        public string IdUsuarioAdicional { get; set; }
        public string AntecedentePatologico { get; set; }
        public string Familiar { get; set; }

        public string AntecedenteParentesco => $"{AntecedentePatologico} - {Familiar}";

    }
}
