using PPS.Estandar;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocNoc.Models
{
    public class Usuario : Entidad
    {
        public string IdUsuario { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public string Telefono { get; set; }
        public string Estatus { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string RutaImagen { get; set; }
        public string RFC { get; set; }
        public string RFCRazonSocial { get; set; }
        public string RFCDireccion { get; set; }
        public string RFCEmail { get; set; }
        public byte IdTipoUsuario { get; set; }
        public bool RecibeNotificacion { get; set; }
        public string CP { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Procedencia { get; set; }
        public string EstadoCivil { get; set; }
        public bool RequiereFactura { get; set; }
        public string RFCEstado { get; set; }
        public string RFCMunicipio { get; set; }
        public string RFCCP { get; set; }
        public string Cedula { get; set; }
        public string Titulo { get; set; }
        public string Universidad { get; set; }
        public int IdEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; }
        public string ExtensionImagen { get; set; }
        public bool AceptaUrgencias { get; set; }
        public string TipoSangre { get; set; }
        public byte EstatusMensaje { get; set; }
        public string IdUsuarioOpenPay { get; set; }
        public string TituloProfesional { get; set; }
        public DateTime ProximaCita { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellidos}";

        public string TituloNombreCompleto => $"{Titulo} {NombreCompleto}";

        public string TituloEspecialidad => $"{TituloProfesional} | {NombreEspecialidad}";

        public string ProximaCitaLetra => ObtenerProximaCitaLetra();

        public string CedulaLetra => $"Ced. Prof. {Cedula}";

        private string ObtenerProximaCitaLetra()
        {
            string fechaLetra = String.Empty;

            if (ProximaCita.Date > DateTime.Now.Date)
            {
                if ((ProximaCita.Date - DateTime.Now.Date).TotalDays == 1)
                    fechaLetra += "Mañana, ";
                else
                    fechaLetra += ProximaCita.ToString("MMMM d, ", CultureInfo.CreateSpecificCulture("es-ES"));
            }
            else
            {
                if (ProximaCita.Date == DateTime.Now.Date)
                    fechaLetra += "Hoy, ";
                else
                    fechaLetra += ProximaCita.ToString("MMMM d, ", CultureInfo.CreateSpecificCulture("es-ES"));
            }

            fechaLetra += ProximaCita.ToString("h:mm tt");

            return fechaLetra;
        }

        public Usuario() : base()
        {

        }

        public Usuario(UsuarioAdicionalAPP _usuario)
        {
            this.IdUsuario = _usuario.IdUsuarioAdicional;
            this.Nombre = _usuario.Nombre;
            this.Apellidos = _usuario.Apellidos;
            this.FechaNacimiento = _usuario.FechaNacimiento;
            this.Sexo = _usuario.Sexo;
            this.Email = _usuario.Email;
            this.Telefono = _usuario.Telefono;
            this.RutaImagen = _usuario.RutaImagen;
            this.ExtensionImagen = _usuario.ExtensionImagen;
            this.Estado = _usuario.Estado;
            this.EstadoCivil = _usuario.EstadoCivil;
        }




    }

}
