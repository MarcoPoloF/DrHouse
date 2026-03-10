using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    /// <summary>
    /// Clase que se utiliza para regresar los reviews de los doctores
    /// </summary>
    public class ParaTraeReviewsDelDoctor
    {
        /// <summary>
        /// Raiting de tipo entero
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// Nombre del paciente que lo calificó
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Fecha de cuando lo calificó
        /// </summary>
        public DateTime FechaRegistro { get; set; }
        /// <summary>
        /// Comentario que pusó
        /// </summary>
        public string Comentario { get; set; }

        public string FechaRegistroLetra => FechaRegistro.ToString("dd MMM, yyyy");
    }
}
