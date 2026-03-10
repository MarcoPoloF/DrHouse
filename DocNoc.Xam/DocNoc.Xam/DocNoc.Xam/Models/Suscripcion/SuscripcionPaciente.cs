using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocNoc.Models
{
    public class SuscripcionPaciente
    {
        public int IdSuscripcion { get; set; }
        public string NombreSuscripcion { get; set; }
        public decimal Precio { get; set; }
        public string IdPlanOpenPay { get; set; }
        public string Beneficios { get; set; }
        public string Periodicidad { get; set; }
        public string Descripcion { get; set; }
        public string SuscripcionActiva { get; set; }
        public bool Activa { get; set; }

        public string PrecioLetra => Precio.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
    }
}
