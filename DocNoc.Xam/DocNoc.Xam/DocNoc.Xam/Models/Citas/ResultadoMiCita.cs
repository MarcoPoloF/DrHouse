using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class ResultadoMiCita : ResultadoMisCitas
    {
        public string Motivo { get; set; }
        public string NombreConsultorio { get; set; }
        public string Direccion { get; set; }
        public string Cedula { get; set; }
        public bool CedulaVerificada { get; set; }
        public int Rating { get; set; }
        public string RutaImagen { get; set; }
        public string IdUsuarioDoctor { get; set; }
        public int IdConsultorio { get; set; }
        public string CodigoQR { get; set; }
        public string IdUsuarioPaciente { get; set; }
        public bool EsPacienteAdicional { get; set; }
        public bool EsPacienteExterno { get; set; }
    }
}
