using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocNoc.Models
{
    public class MiMedicamento
    {
        public int IdMedicamento { get; set; }
        public DateTime FechaToma { get; set; }
        public string CuidadoRecomendacion { get; set; }
        public string Medicamento { get; set; }
        public bool Tomado { get; set; }
        public string Doctor { get; set; }
        public string Paciente { get; set; }
        public string IdPaciente { get; set; }
        public bool EsPacienteAdicional { get; set; }

        public string FechaLetra => ObtenerFechaLetra();
        public string IndicadoPor => string.IsNullOrWhiteSpace(Doctor) ? $"Indicado por: Mí" : $"Indicado por: {Doctor}";
        public string RecetadoA => $"Recetado a: {Paciente}";

        public bool HayIndicaciones => string.IsNullOrWhiteSpace(CuidadoRecomendacion) ? false : true;
        public bool HayDoctor => string.IsNullOrWhiteSpace(Doctor) ? false : true;

        private string ObtenerFechaLetra()
        {
            string fechaLetra = string.Empty;

            if (FechaToma.Date == DateTime.Now.Date)
                fechaLetra = $"Hoy, {FechaToma.ToString("h:mm tt", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper()}";
            else
                fechaLetra = FechaToma.ToString("dd MMMM, h:mm tt", CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();

            return fechaLetra;
        }
    }
}
