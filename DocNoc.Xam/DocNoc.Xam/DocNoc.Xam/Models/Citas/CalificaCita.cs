using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class CalificaCita
    {
        public string IdUsuarioPaciente { get; set; }
        public string IdUsuarioDoctor { get; set; }
        public int IdCita { get; set; }
        public byte IdPregunta1 { get; set; }
        public byte IdPregunta2 { get; set; }
        public byte IdPregunta3 { get; set; }
        public int Calificacion1 { get; set; }
        public int Calificacion2 { get; set; }
        public int Calificacion3 { get; set; }
    }
}
