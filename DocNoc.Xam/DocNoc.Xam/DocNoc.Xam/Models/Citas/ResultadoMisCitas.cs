using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DocNoc.Models
{
    public class ResultadoMisCitas
    {
        public string Estatus { get; set; }
        public DateTime FechaCitaI { get; set; }
        public string Paciente { get; set; }
        public string Doctor { get; set; }
        public string NombreEspecialidad { get; set; }
        public int IdCita { get; set; }

        public bool MostrarDia { get; set; }

        public string DiaFecha => FechaCitaI.ToString("dd");
        public string MesFecha => FechaCitaI.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")).Substring(0, 3).ToUpper();

        public string EstatusCita => ObtenerEstatusCita();

        public Color ColorEstatus => Estatus == "Confirmada" ? Color.FromHex("#9EBDF7") : Color.FromHex("#4EF5C0");

        public string HoraLetra => FechaCitaI.ToString("h:mm tt").ToUpper();
        public string FechaLetra => $"{DiaFecha}-{MesFecha} {HoraLetra}";

        public string PacienteCita => Paciente == " " ? "Mi Cita" : $"Cita de {Paciente}";

        public string PacienteLetra => Paciente == " " ? "Yo" : $"{Paciente}";

        private string ObtenerEstatusCita()
        {
            switch (Estatus)
            {
                case "Confirmada":
                    return "Cita Confirmada";
                case "PorAceptar":
                    return "Cita pendiente de aceptación";
                case "Rechazada":
                    return "Cita rechazada por el médico";
                default:
                    return "Estatus indefinido";
            }
        }
    }
}
