using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Xam.Models.Text
{
    /// <summary>
    /// Definición de textos de página: Login (dn-04-3)
    /// </summary>
    public class LoginTxt
    {
        public string BotonLogin { get; set; }
        public string BotonRegistro { get; set; }
        public string BotonContrasena { get; set; }
        public string EmailPlaceholder { get; set; }
        public string PasswordPlaceholder { get; set; }
        public string LoginError { get; set; }
    }
}
