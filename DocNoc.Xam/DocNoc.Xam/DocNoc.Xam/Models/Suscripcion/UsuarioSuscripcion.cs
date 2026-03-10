using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class UsuarioSuscripcion
    {
        public string IdUsuarioSuscripcion { get; set; }
        public DateTime VigenciaInicio { get; set; }
        public DateTime VigenciaFin { get; set; }
        public string NombreSuscripcion { get; set; }
        public decimal Precio { get; set; }
        public string Periodicidad { get; set; }
        public byte ConsultoriosIncluidos { get; set; }
        public byte AsistentesIncluidos { get; set; }
        public byte ExpedientesAdicionales { get; set; }
        public short EspacioAlmacenamiento { get; set; }
        public string Beneficio4 { get; set; }
        public string Beneficio5 { get; set; }
        public string Descripcion { get; set; }
        public bool TieneSuscripcion { get; set; }
        public bool EsBasica { get; set; }
    }
}
