using PPS.Estandar;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class Especialidad : Entidad
    {
        public int IdEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; }
        public string Descripcion { get; set; }
        public string RutaMedias { get; set; }
        public string Estatus { get; set; }
        public string ExtensionMedios { get; set; } //verificar y ver la posibilidad de eliminar

    }
}
