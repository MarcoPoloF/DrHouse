using PPS.Estandar;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class JsonWebToken : Entidad
    {
        public string JWT { get; set; }
        public bool EsSistemas { get; set; }
        public bool EsAdministrador { get; set; }
        public bool EsDuenio { get; set; }
        public bool EsUsuarioDuenio { get; set; }
        public bool EsCliente { get; set; }
    }
}
