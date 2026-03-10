using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class DetalleFecha
    {
        public DateTime Fecha { get; set; }

        public string DiaSemana => new System.Globalization.CultureInfo("es-ES").DateTimeFormat.GetDayName(Fecha.DayOfWeek).Substring(0,3).ToUpper();
        public string FechaDia => Fecha.ToString("dd");

        public DetalleFecha(DateTime fecha)
        {
            Fecha = fecha;
        }
    }
}
