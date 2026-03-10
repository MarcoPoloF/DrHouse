using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class UsuarioPago
    {
        public string IdUsuario { get; set; }
        public string Tipo { get; set; }
        public string Terminacion { get; set; }
        public bool Predeterminada { get; set; }
        public string IdPagoOpenPay { get; set; }
        public string Brand { get; set; }

        public string NumeroTarjeta => $"XXXX  XXXX  XXXX  {Terminacion}";

        public string NumeroTarjetaCorto => $"**** {Terminacion}";

        public string IconoTarjeta => $"{Brand}.png";

        public string TipoLetra => Tipo == "credit" ? "TARJETA DE CRÉDITO" : Tipo == "debit" ? "TARJETA DE DÉBITO" : "NO DEFINIDO";
    }
}
