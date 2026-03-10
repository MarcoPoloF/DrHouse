using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ParaCrearCita
    {
        public int IdConsultorio { get; set; }
        public DateTime FechaCitaI { get; set; }
        public string IdUsuarioPaciente { get; set; }
        public string IdUsuarioDoctor { get; set; }
        public string TipoCita { get; set; } // 1 Cita, 2 Receta
        public string Motivo { get; set; }
        public int IdEspecialidad { get; set; }
        public string NombreInvitado { get; set; }
        public string ApellidosInvitado { get; set; }
        public string IdUsuarioAdicional { get; set; }
    }
}
