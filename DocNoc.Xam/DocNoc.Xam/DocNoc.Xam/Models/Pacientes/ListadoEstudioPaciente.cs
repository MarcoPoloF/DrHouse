using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ListadoEstudioPaciente
    {
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string RutaImagen { get; set; }
    }
}
