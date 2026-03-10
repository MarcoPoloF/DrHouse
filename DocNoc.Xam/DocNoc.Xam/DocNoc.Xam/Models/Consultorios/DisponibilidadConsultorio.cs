using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class DisponibilidadConsultorio
    {
        public int IdConsultorio { get; set; }
        public string NombreConsultorio { get; set; }
        public string Direccion { get; set; }
        public List<CitaConsultorio> CitasDisponibles { get; set; }
        public CitaConsultorio CitaSeleccionada { get; set; }

        public bool ConConsultas => (CitasDisponibles != null && CitasDisponibles.Count > 0) ? true : false;
        public bool SinConsultas => !ConConsultas;
    }
}
