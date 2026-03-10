using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ExpedienteHistoriaClinica
    {
        public string IdUsuarioPaciente { get; set; }
        public string IdUsuarioAdicional { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public string Motivo { get; set; }
        public string Comentarios { get; set; }
        public string FechaLetra => Fecha.ToString("dd/MM/yyyy");
    }
}
