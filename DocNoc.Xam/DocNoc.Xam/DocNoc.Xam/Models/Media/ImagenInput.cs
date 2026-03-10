using PPS.Estandar;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;

namespace DocNoc.Models
{
    public class ImagenInput : Entidad
    {
        public ImageSource Imagen { get; set; }
        public string ImagenBase64 { get; set; }
        public string NombreImagen { get; set; }
        public string ExtensionImagen { get; set; }

        public ImagenInput() : base()
        {

        }
    }
}
