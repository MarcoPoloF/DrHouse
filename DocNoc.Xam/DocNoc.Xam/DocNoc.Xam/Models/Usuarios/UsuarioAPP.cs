using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class UsuarioAPP : Usuario
    {
        public string AcercadeMi { get; set; }
        public int Rating { get; set; }
        public bool CedulaVerificada { get; set; }
        public int AnioEgreso { get; set; }
        public byte ExperienciaEspecialidad { get; set; }
        public string RutaPerfil { get; set; }

    }
}
