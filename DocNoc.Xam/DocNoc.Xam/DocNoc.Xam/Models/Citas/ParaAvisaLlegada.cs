using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ParaAvisaLlegada
    {
        public int IdCita { get; set; }
        public string IdUsuarioEnvia { get; set; }
        public string IdUsuarioRecibe { get; set; }
        public string TextoMensaje { get; set; }
    }
}