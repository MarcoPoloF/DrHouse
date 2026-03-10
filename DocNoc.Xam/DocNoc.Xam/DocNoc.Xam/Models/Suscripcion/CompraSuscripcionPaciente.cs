using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class CompraSuscripcionPaciente
    {
        public string IdUsuario { get; set; }
        public string IdPlanOpenPay { get; set; }
        public int IdSuscripcion { get; set; }
        public string IdPagoOpenPay { get; set; }
        public string IdUsuarioOpenPay { get; set; }
    }
}
