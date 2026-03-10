using PPS.Estandar;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class Anuncio : Entidad
    {
        public int IdAnuncio { get; set; }
        public byte IdTipoUsuario { get; set; } //-- 0 TODOS, 1 Pacientes, 2 Medicos
        public string NombreAnuncio { get; set; }
        public DateTime VigenciaInicio { get; set; }
        public DateTime VigenciaFinal { get; set; }
        public string RutaMedios { get; set; }
        public string Estatus { get; set; } //ACTIVO, PAUSADO, ELIMINADO
        public string ExtensionMedios { get; set; } //verificar y ver la posibilidad de eliminar

    }
}
