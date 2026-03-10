using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class EstudioAnalisisPaciente
    {
        public int IdExpedienteImagen { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Archivo { get; set; }
        public bool Compartir { get; set; }
        public string FechaLetra => Fecha.ToString("dd/MM/yyyy");
    }
}
