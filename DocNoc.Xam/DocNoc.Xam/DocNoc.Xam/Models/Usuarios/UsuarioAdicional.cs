using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class UsuarioAdicional
    {
        public string IdUsuario { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string Telefono { get; set; }
        public string Estatus { get; set; }
        public string RutaImagen { get; set; }
    }
}
