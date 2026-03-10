using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class InsertarMiMedicamento
    {
        public string IdUsuario { get; set; }
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string CuidadoRecomendacion { get; set; }
        public DateTime FechaPrimerToma { get; set; }
        public byte TomarCada { get; set; }
        public byte Duracion { get; set; }
        public string IdUsuarioAdicional { get; set; }
        public DateTime? FechaDispositivo { get; set; }
    }

    public class InsertarMiMedicamento2 : InsertarMiMedicamento
    {
        public string IdUsuarioPrincipal { get; set; }
    }

    public class InsertarMedicamentoProgramacion : InsertarMiMedicamento
    {
        public int IdMedicamento  { get; set; }
    }

    public class InsertarMedicamentoProgramacion2 : InsertarMedicamentoProgramacion
    {
        public string IdUsuarioPrincipal { get; set; }
    }
}
