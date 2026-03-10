using System;
using System.Collections.Generic;
using System.Text;

namespace DocNoc.Models
{
    public class Favorito
    {
        public string IdUsuario { get; set; }
        public string IdUsuarioDoctor { get; set; }
    }

    public class ListaFavoritos : Favorito
    {
        public string NombreDoctor { get; set; }
        public string Cedula { get; set; }
        public bool CedulaVerificada { get; set; }
        public int Rating { get; set; }
        public string RutaImagen { get; set; }
        public string Titulo { get; set; }
        public string CedulaLetra => $"Ced. Prof. {Cedula}";
        public string NombreCompletoTitulo => $"{Titulo} {NombreDoctor}";
        public string NombreEspecialidad { get; set; }
        public string TituloProfesional { get; set; }
        public string TituloEspecialidad => $"{TituloProfesional} | {NombreEspecialidad}";
        public string RutaPerfil { get; set; }
    }

}
