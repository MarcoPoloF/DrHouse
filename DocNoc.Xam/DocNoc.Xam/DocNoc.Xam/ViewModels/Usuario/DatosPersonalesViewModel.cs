using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DocNoc.Xam.ViewModels.Usuarios
{
    /// <summary>
    /// Definición de ViewModel: Datos Personales (dn-37-3).
    /// </summary>
    public class DatosPersonalesViewModel : DocNocViewModel
    {
        #region Constructor

        public DatosPersonalesViewModel (INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Home (dn-07-3).
            //PageText = text.Get<PaginaTxt>("dn-07-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            this.BackCommand = new Command(Regresar);
            this.PopupCommand = new Command<SfPopupLayout>(AbrirPopup);
            this.CancelCommand = new Command(CargarDatos);
            this.UpdateCommand = new Command(ActualizarUsuario);
            this.CheckChangesCommand = new Command(ValidarCambios);
            this.EditCommand = new Command(CargarImagenArchivo);

            ValidarTipoUsuario();
        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;

        private Usuario usuarioSinCambios;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the page text.
        /// </summary>
        //public PaginaTxt PageText
        //{
        //    get { return this.pageText; }
        //    set { SetProperty(ref pageText, value); }
        //}

        public Usuario Usuario
        {
            get { return this.usuario; }
            set { SetProperty(ref usuario, value); }
        }
        private Usuario usuario;
        
        public List<string> OpcionesSexo
        {
            get { return this.opcionesSexo; }
            set { SetProperty(ref opcionesSexo, value); }
        }
        private List<string> opcionesSexo;

        public string SexoSeleccionado
        {
            get { return this.sexoSeleccionado; }
            set { SetProperty(ref sexoSeleccionado, value); }
        }
        private string sexoSeleccionado;

        public List<string> OpcionesEstadoCivil
        {
            get { return this.opcionesEstadoCivil; }
            set { SetProperty(ref opcionesEstadoCivil, value); }
        }
        private List<string> opcionesEstadoCivil;

        public string EstadoCivilSeleccionado
        {
            get { return this.estadoCivilSeleccionado; }
            set { SetProperty(ref estadoCivilSeleccionado, value); }
        }
        private string estadoCivilSeleccionado;

        public bool HayCambios
        {
            get { return this.hayCambios; }
            set { SetProperty(ref hayCambios, value); }
        }
        private bool hayCambios;

        private bool esUsuarioAdicional;

        #endregion

        #region Commands

        public Command CancelCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command UpdateCommand { get; set; }

        public Command CheckChangesCommand { get; set; }

        public Command EditCommand { get; set; }

        #endregion

        #region Methods

        private async void ActualizarUsuario()
        {
            IsBusy = true;

            if (!EsUsuarioAdicional)
            {
                Usuario.Sexo = SexoSeleccionado.Substring(0, 1);
                Usuario.EstadoCivil = EstadoCivilSeleccionado;

                var respuestaApi = await DocNocApi.Usuarios.ActualizaPerfil(this.Usuario);

                if (respuestaApi.Error)
                {
                    ErrorEntidad(respuestaApi);
                    IsBusy = false;
                    return;
                }
            }
            else
            {
                var usuarioAdicional = new UsuarioAdicionalAPP()
                {
                    IdUsuario = idUsuario,
                    IdUsuarioAdicional = IdExpediente,
                    Nombre = this.Usuario.Nombre,
                    Apellidos = this.Usuario.Apellidos,
                    FechaNacimiento = this.Usuario.FechaNacimiento.GetValueOrDefault(),
                    Sexo = SexoSeleccionado.Substring(0,1),
                    EstadoCivil = EstadoCivilSeleccionado,
                    Email = this.Usuario.Email,
                    Telefono = this.Usuario.Telefono,
                    Estado = this.Usuario.Estado
                };

                var respuestaApi = await DocNocApi.Usuarios.ActualizaUsuarioAdicional(usuarioAdicional);

                if (respuestaApi.Error)
                {
                    ErrorEntidad(respuestaApi);
                    IsBusy = false;
                    return;
                }
            }

            IsBusy = false;

            CargarDatos();
        }

        private async void CargarDatos()
        {
            if (!EsUsuarioAdicional)
            {
                var respuestaApi = await DocNocApi.Usuarios.TraeUsuario(new ParaFiltroUsuario() { IdUsuario = IdExpediente });

                if (respuestaApi.Error)
                {
                    ErrorEntidad(respuestaApi);
                    return;
                }

                Usuario = respuestaApi.Contenido;
            }
            else
            {
                var respuestaApi = await DocNocApi.Usuarios.TraeUsuarioAdicional(new ParaFiltroUsuario() { IdUsuario = IdExpediente });

                if (respuestaApi.Error)
                {
                    ErrorEntidad(respuestaApi);
                    return;
                }

                Usuario = new Usuario(respuestaApi.Contenido);
            }

            if (Usuario.Error)
            {
                ErrorEntidad(Usuario);
                return;
            }

            usuarioSinCambios = new Usuario()
            {
                Nombre = Usuario.Nombre,
                Apellidos = Usuario.Apellidos,
                FechaNacimiento = Usuario.FechaNacimiento,
                Sexo = Usuario.Sexo,
                EstadoCivil = Usuario.EstadoCivil,
                Email = Usuario.Email,
                Telefono = Usuario.Telefono
            };

            OpcionesSexo = new List<string>() { "Femenino", "Masculino" };

            if(usuarioSinCambios.Sexo == "F")
            {
                SexoSeleccionado = OpcionesSexo[0];
            }
            else
            {
                SexoSeleccionado = OpcionesSexo[1];
            }

            OpcionesEstadoCivil = new List<string>() { "Soltero", "Casado" };

            EstadoCivilSeleccionado = usuarioSinCambios.EstadoCivil;

            HayCambios = false;
        }

        private async void CargarImagenArchivo()
        {
            //if (EsUsuarioAdicional)
            //    return;

            var img = await CargarImagen($"ImagenPerfil-{Usuario.Nombre.Replace(" ", "")}");

            if (img is null)
                return;

            if (img.Error)
            {
                MensajeError(img.CadenaErrores());
                return;
            }

            if (!EsUsuarioAdicional)
            {
                var usuarioImagen = new Usuario()
                {
                    IdUsuario = Usuario.IdUsuario,
                    RutaImagen = img.ImagenBase64,
                    ExtensionImagen = img.ExtensionImagen
                };

                var respuestaApi = await DocNocApi.Usuarios.ActualizaFotoPerfil(usuarioImagen);

                if (respuestaApi.Error)
                {
                    ErrorEntidad(respuestaApi);
                    return;
                }
            }
            else
            {
                var usuarioImagen = new UsuarioAdicionalAPP()
                {
                    IdUsuario = idUsuario,
                    IdUsuarioAdicional = IdExpediente,
                    RutaImagen = img.ImagenBase64,
                    ExtensionImagen = img.ExtensionImagen
                };

                var respuestaApi = await DocNocApi.Usuarios.ActualizaFotoPerfilAdicional(usuarioImagen);

                if (respuestaApi.Error)
                {
                    ErrorEntidad(respuestaApi);
                    return;
                }
            }

            CargarDatos();

        }

        private void ValidarCambios()
        {
            if (Usuario == null)
                return;

            if (usuarioSinCambios == null)
                return;

            if (Usuario != usuarioSinCambios)
                HayCambios = true;
            else
                HayCambios = false;

            if(EstadoCivilSeleccionado != usuarioSinCambios.EstadoCivil)
            {
                HayCambios = true;
            }

            string opcionSexo = "F";

            if(SexoSeleccionado == "Masculino")
            {
                opcionSexo = "M";
            }

            if(opcionSexo != usuarioSinCambios.Sexo)
            {
                HayCambios = true;
            }
        }

        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}
