using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls.Maps;

namespace DocNoc.Xam.Custom.Maps
{
    public class CustomPin : Pin
    {
        public string IdMedico { get; set; }
        public string NombreMedico { get; set; }
        public string NombreEspecialidad { get; set; }
        public string RutaImagen { get; set; }
    }
}
