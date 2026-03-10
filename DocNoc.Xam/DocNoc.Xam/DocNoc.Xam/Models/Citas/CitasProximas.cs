using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocNoc.Models
{
    public class CitasProximas
    {
        public string Paciente { get; set; }
        public DateTime FechaCitaI { get; set; }
        public string Doctor { get; set; }
        public string DireccionConsultorio { get; set; }
        public string NombreEspecialidad { get; set; }
        public int IdCita { get; set; }

        public string Fecha => FechaCitaI.ToString("dd MMMM", CultureInfo.CreateSpecificCulture("es-ES")).Substring(0,6).ToUpper();
        public string Hora => FechaCitaI.ToString("h:mm tt", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
        public string CitaLetra => string.IsNullOrWhiteSpace(Paciente) ? "Mi Cita" : $"Cita de {Paciente}";
    }
}
