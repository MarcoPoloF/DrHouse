using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class AgregaArchivosExpediente : ListadoEstudioPaciente
    {
        public string IdUsuarioPaciente { get; set; }
        public string IdUsuarioDoctor { get; set; }
        public string IdUsuarioEnvia { get; set; }
        public string ExtensionImagen { get; set; }
    }
}
