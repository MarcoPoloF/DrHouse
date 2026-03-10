using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocNoc.Models
{
    public class UsuarioServicioTratamiento
    {
        public string IdUsuario { get; set; }
        public string ServicioTratamiento { get; set; }
        public decimal Precio { get; set; }

        public string PrecioLetra => Precio.ToString("C", CultureInfo.CreateSpecificCulture("es-MX"));
    }
}
