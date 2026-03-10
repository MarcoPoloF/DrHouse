using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class MiMedicamentoAPP
    {
        public int IdMedicamento { get; set; }
        public DateTime FechaPrimerToma { get; set; }
        public string Medico { get; set; }
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string CuidadoRecomendacion { get; set; }
        public bool Programado { get; set; }
        public string FechaLetra => FechaPrimerToma.ToString("dd/MM/yyyy");
        public bool TraeMedico { get; set; }
    }
}
