using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocNoc.Models
{
    public class ResultadoBusquedaMedicoAPP
    {
        public int IdConsultorio { get; set; }
        public string IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string NombreEspecialidad { get; set; }
        public string Cedula { get; set; }
        public bool CedulaVerificada { get; set; }
        public int Rating { get; set; }
        public DateTime ProximaCita { get; set; }
        public string RutaImagen { get; set; }
        public int MedicosDisponibles { get; set; }
        public string Apellidos { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Titulo { get; set; }
        public string NombreConsultorio { get; set; }
        public string TituloProfesional { get; set; }

        public string CedulaLetra => $"Ced. Prof. {Cedula}";

        public string ProximaCitaLetra => ObtenerProximaCitaLetra();

        public string NombreCompleto => $"{Nombre} {Apellidos}";

        public string NombreCompletoTitulo => $"{Titulo} {NombreCompleto}";

        public string TituloEspecialidad => $"{TituloProfesional} | {NombreEspecialidad}";

        private string ObtenerProximaCitaLetra()
        {
            string fechaLetra = String.Empty;

            if (ProximaCita.Date > DateTime.Now.Date)
            {
                if ((ProximaCita.Date - DateTime.Now.Date).TotalDays == 1)
                    fechaLetra += "Mañana, ";
                else
                    fechaLetra += ProximaCita.ToString("MMM d, ", CultureInfo.CreateSpecificCulture("es-ES"));
            }
            else
            {
                if (ProximaCita.Date == DateTime.Now.Date)
                    fechaLetra += "Hoy, ";
                else
                    fechaLetra += ProximaCita.ToString("MMM d, ", CultureInfo.CreateSpecificCulture("es-ES"));
            }

            fechaLetra += ProximaCita.ToString("h:mm tt");

            return fechaLetra;
        }
    }
}
