using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class UsuarioAdicionalAPP
    {
        public string IdUsuarioAdicional { get; set; }
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
        public DateTime FechaRegistro { get; set; }
        public string TipoSangre { get; set; }
        public string ExtensionImagen { get; set; }
        public string Estado { get; set; }

        public UsuarioAdicionalAPP() { }

        public UsuarioAdicionalAPP(Usuario _usuario, string idUsuario)
        {
            this.IdUsuario = idUsuario;
            this.IdUsuarioAdicional = _usuario.IdUsuario;
            this.Nombre = _usuario.Nombre;
            this.Apellidos = _usuario.Apellidos;
            this.FechaNacimiento = _usuario.FechaNacimiento.GetValueOrDefault();
            this.Sexo = _usuario.Sexo;
            this.Email = _usuario.Email;
            this.Telefono = _usuario.Telefono;
            this.Estado = _usuario.Estado;
        }
    }
}
