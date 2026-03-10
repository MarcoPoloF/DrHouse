using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class UsuarioHistoriaClinica2 : UsuarioHistoriaClinica
    {
        public string TipoEvento { get; set; }
        public string FechaLetra => Fecha.ToString("dd/MM/yyyy");
    }
}
