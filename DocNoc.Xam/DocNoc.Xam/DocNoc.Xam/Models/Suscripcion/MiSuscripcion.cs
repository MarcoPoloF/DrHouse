using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class MiSuscripcion
    {
        public string IdUsuario { get; set; }
        public string NombreSuscripcion { get; set; }
        public string SeRenovara { get; set; }
        public decimal Precio { get; set; }
        public bool Activa { get; set; }
        public string IdSuscripcion { get; set; }
        public string IdOpenPay { get; set; }
        public string IdPlanOpenPay { get; set; }
    }
}
