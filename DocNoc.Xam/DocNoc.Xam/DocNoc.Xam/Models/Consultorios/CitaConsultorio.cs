using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocNoc.Models
{
    public class CitaConsultorio
    {
        public int IdConsultorio { get; set; }
        public DateTime FechaCitaI { get; set; }

        public string HoraConsulta => FechaCitaI.ToString("HH:mm");

        public string FechaPicker => FechaCitaI.Date == DateTime.Now.Date
            ? "Hoy, " + FechaCitaI.ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper()
            : FechaCitaI.ToString("MMMM dd, hh:mm tt", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
    }
}
