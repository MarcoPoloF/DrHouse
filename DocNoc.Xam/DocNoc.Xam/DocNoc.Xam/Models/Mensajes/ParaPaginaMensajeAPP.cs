using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocNoc.Models
{
    public class ParaPaginaMensajeAPP
    {
        public string IdUsuario { get; set; }
        public string RutaImagen { get; set; }
        public byte EstatusMensaje { get; set; }
        public string Nombre { get; set; }
        public string NombreEspecialidad { get; set; }
        public DateTime Fecha { get; set; }

        public string HoraLetra => ObtenerHoraLetra();
        public string EstatusMensajeColor => ObtenerEstatusMensajeColor();

        private string ObtenerHoraLetra()
        {
            string horaLetra = string.Empty;

            if(Fecha.Date < DateTime.Now.Date)
            {
                horaLetra = Fecha.ToString("dd/MM");
            }
            else
            {
                if(Fecha >= DateTime.Now)
                {
                    horaLetra = "Ahora";
                }
                else
                {
                    TimeSpan diferencia = DateTime.Now - Fecha;
                    double diferenciaMinutos = diferencia.TotalMinutes;

                    if(diferenciaMinutos < 1)
                    {
                        horaLetra = "Ahora";
                    }
                    else
                    {
                        if (diferenciaMinutos < 60)
                        {
                            horaLetra = diferenciaMinutos.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES")) + " m";
                        }
                        else
                        {
                            double diferenciaHoras = diferenciaMinutos / 60;
                            horaLetra = diferenciaHoras.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES")) + " h";
                        }
                    }
                }
            }

            return horaLetra;
        }

        
        private string ObtenerEstatusMensajeColor()
        {
            string color = string.Empty;
            switch (EstatusMensaje)
            {
                case 3:
                    color = "Gold";
                    break;
                case 2:
                    color = "Red";
                    break;
                case 1:
                    color = "Green";
                    break;
            }
            return color;
        }

    }
}
