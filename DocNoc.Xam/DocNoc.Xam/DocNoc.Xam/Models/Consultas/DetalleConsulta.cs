using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class DetalleConsulta : Consulta
    {
        public string Titulo { get; set; }
        public string NombreDoctor { get; set; }
        public string NombreEspecialidad { get; set; }
        public string Cedula { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public bool CedulaVerificada { get; set; }
        public int Rating { get; set; }
        public string RutaImagen { get; set; }
        public string IdUsuarioDoctor { get; set; }

        public string NombreCompletoTitulo => $"{Titulo} {NombreDoctor}";
    }
}
