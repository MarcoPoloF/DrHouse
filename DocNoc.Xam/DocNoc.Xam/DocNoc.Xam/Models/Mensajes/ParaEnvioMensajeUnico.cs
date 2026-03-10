using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ParaEnvioMensajeUnico
    {
        public string IdUsuarioEnvia { get; set; }
        public string IdUsuarioRecibe { get; set; }
        public string TextoMensaje { get; set; }
    }
}
