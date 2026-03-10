using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ParaComparteArchivoExpediente
    {
        public string IdUsuarioEnvia { get; set; }
        public string IdUsuarioRecibe { get; set; }
        public int IdExpedienteImagen { get; set; }
        public DateTime CompartidoHasta { get; set; }
    }
}
