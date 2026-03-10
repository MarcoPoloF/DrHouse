using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class RegistroUsuario
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public byte IdTipoUsuario { get; set; }
        public string Procedencia { get; set; }

    }
}
