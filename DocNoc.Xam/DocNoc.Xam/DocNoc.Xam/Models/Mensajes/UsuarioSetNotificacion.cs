using PPS.Estandar;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class UsuarioSetNotificacion : Entidad
    {
        public byte IdParametroNotificacion { get; set; }
        public string IdUsuario { get; set; }
        public bool Email { get; set; }
        public bool AppWeb { get; set; }
        public string TipoNotificacion { get; set; }
    }
}
