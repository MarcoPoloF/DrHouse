using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class UsuarioHistoriaClinica
    {
        public string IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public byte IdTipoEvento { get; set; }
        public string Motivo { get; set; }
        public string Comentarios { get; set; }

    }
}
