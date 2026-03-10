using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocNoc.Models
{
    public class TraeAgendasMedicoAPP
    {
        public int IdConsultorio { get; set; }
        public string IdUsuario { get; set; }
        public DateTime FechaCitaI { get; set; }
        public string NombreConsultorio { get; set; }
        public string CalleNumero { get; set; }

        public string HoraConsulta => FechaCitaI.ToString("hh:mm");

        public string FechaPicker => FechaCitaI.Date == DateTime.Now.Date 
            ? "Hoy, " + FechaCitaI.ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper()
            : FechaCitaI.ToString("MMMM dd, hh:mm tt", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
    }
}
