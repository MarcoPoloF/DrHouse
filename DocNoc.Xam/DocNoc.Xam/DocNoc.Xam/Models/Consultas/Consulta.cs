using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocNoc.Models
{
    public class Consulta
    {
        public int IdConsulta { get; set; }
        public int IdCita { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string NombrePaciente { get; set; }
        public string Motivo { get; set; }
        public bool Calificada { get; set; }

        public string ConsultaPaciente => NombrePaciente == " " ? "Mi Cita" : $"Cita de {NombrePaciente}";

        public string MotivoLetra => $"Motivo de Consulta: {Motivo}";

        public string DiaFecha => FechaConsulta.ToString("dd");
        public string MesFecha => FechaConsulta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")).Substring(0, 3).ToUpper();
        public string HoraLetra => FechaConsulta.ToString("h:mm tt").ToUpper();
        public string FechaLetra => $"{DiaFecha}-{MesFecha} {HoraLetra}";
    }
}
