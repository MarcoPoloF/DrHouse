using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class AltaTarjetaParaPaciente
    {
        public string IdUsuario { get; set; }
        public string HolderName { get; set; }
        public string CardNumber { get; set; }
        public string Cvv2 { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string DeviceSessionId { get; set; }
        public string TokenID { get; set; }

    }
}
