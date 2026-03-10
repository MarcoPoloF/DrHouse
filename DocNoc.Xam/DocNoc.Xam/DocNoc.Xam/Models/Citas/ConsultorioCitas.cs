using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ConsultorioCitas
    {
        public int IdConsultorio { get; set; }
        public string NombreConsultorio { get; set; }
        public List<TraeAgendasMedicoAPP> Consultas { get; set; }

        public ConsultorioCitas()
        {
            Consultas = new List<TraeAgendasMedicoAPP>();
        }
    }
}
