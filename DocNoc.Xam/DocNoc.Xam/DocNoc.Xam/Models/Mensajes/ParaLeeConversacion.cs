using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ParaLeeConversacion
    {
        public int IdMensaje { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioEnvia { get; set; }
        public string NombreEnvia { get; set; }
        public string IdUsuarioRecibe { get; set; }
        public string NombreRecibe { get; set; }
        public string Estatus { get; set; } // --NOLEIDO, LEIDO, ARCHIVADO, ELIMINADO
        public DateTime FechaEnvio { get; set; }
        public string TextoMensaje { get; set; }
        public string EnviadoRecibido { get; set; }
        public string RutaImagen { get; set; }
        public string TipoUsuario { get; set; }
        public DateTime ProximaCita { get; set; }
        public DateTime UltimaCita { get; set; }
    }
}
