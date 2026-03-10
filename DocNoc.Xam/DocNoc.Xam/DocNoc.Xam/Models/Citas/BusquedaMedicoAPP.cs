using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class BusquedaMedicoAPP
    {
        public string CiudadOCP { get; set; }
        public int IdEspecialidad { get; set; }
        public string Cita { get; set; } //Hoy, Semana, Mes
        public byte? IdAseguradora { get; set; }
        public int? Calificacion { get; set; }
        public bool? AceptaCitaAutomaticamente { get; set; }
    }
}
