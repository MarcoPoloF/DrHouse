using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ParaFiltroUsuario
    {
        public string IdUsuario { get; set; }
        public string IdUsuarioAdicional { get; set; }
    }

    public class ParaFiltroUsuario2
    {
        public string IdUsuario { get; set; }
        public string IdUsuarioPrincipal { get; set; }
    }

    public class ParaFiltroUsuarioyDato : ParaFiltroUsuario
    {
        public string Dato { get; set; }
    }

}