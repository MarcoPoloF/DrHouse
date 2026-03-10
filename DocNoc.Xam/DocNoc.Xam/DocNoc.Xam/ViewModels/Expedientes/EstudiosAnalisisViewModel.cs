using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Expedientes
{
    /// <summary>
    /// Definición de ViewModel: Estudios y Análisis (dn-47-3).
    /// </summary>
    public class EstudiosAnalisisViewModel : DocNocViewModel
    {
        #region Constructor

        public EstudiosAnalisisViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.PopupCommand = new Command<SfPopup>(AbrirPopup);
            this.ImagePopupCommand = new Command<string>(AbrirImagen);
            this.AddCommand = new Command(InsertarEvento);
            this.LoadImageCommand = new Command(CargarImagenArchivo);
            this.OpenShareCommand = new Command(AbrirCompartir);
            this.CompartirArchivosCommand = new Command(CompartirArchivos);
            this.IrSuscripcionCommand = new Command(AbrirSuscripcion);
            this.AgregarPopupCommand = new Command(AgregarRegistro);

            CompartirHabilitado = false;

            ValidarTipoUsuario();

            idUsuarioExpediente = IdExpediente;

            //
            if(Preferences.Get("CompartirArchivos_Cita") == "true")
            {
                Preferences.Set("CompartirArchivos_Cita", string.Empty);

                idUsuarioExpediente = Preferences.Get("IdExpediente_CompartirArchivos");
                idUsuarioMedico = Preferences.Get("IdMedico_CompartirArchivos");

                CompartirHabilitado = true;
                CompartirHasta = DateTime.Now.Date;
            }
        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;

        private string idUsuarioExpediente;

        private string idUsuarioMedico;

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

        public List<EstudioAnalisisPaciente> Registros
        {
            get { return this.registros; }
            set { SetProperty(ref registros, value); }
        }
        private List<EstudioAnalisisPaciente> registros;

        public List<TiposEventos> TiposEvento
        {
            get { return this.tiposEvento; }
            set { SetProperty(ref tiposEvento, value); }
        }
        private List<TiposEventos> tiposEvento;

        public DateTime FechaEvento
        {
            get { return this.fechaEvento; }
            set { SetProperty(ref fechaEvento, value); }
        }
        private DateTime fechaEvento;

        public DateTime CompartirHasta
        {
            get { return this.compartirHasta; }
            set { SetProperty(ref compartirHasta, value); }
        }
        private DateTime compartirHasta;

        public TiposEventos Tipo
        {
            get { return this.tipo; }
            set { SetProperty(ref tipo, value); }
        }
        private TiposEventos tipo;

        public string Descripcion
        {
            get { return this.descripcion; }
            set { SetProperty(ref descripcion, value); }
        }
        private string descripcion;

        public string InformacionCompartir
        {
            get { return this.informacionCompartir; }
            set { SetProperty(ref informacionCompartir, value); }
        }
        private string informacionCompartir;

        public ImagenInput Imagen
        {
            get { return this.imagen; }
            set { SetProperty(ref imagen, value); }
        }
        private ImagenInput imagen;

        public ImageSource FuenteImagen
        {
            get { return this.fuenteImagen; }
            set { SetProperty(ref fuenteImagen, value); }
        }
        private ImageSource fuenteImagen;

        public bool CompartirVisible
        {
            get { return this.compartirVisible; }
            set { SetProperty(ref compartirVisible, value); }
        }
        private bool compartirVisible;

        public bool ImagenVisible
        {
            get { return this.imagenVisible; }
            set { SetProperty(ref imagenVisible, value); }
        }
        private bool imagenVisible;

        public bool CompartirHabilitado
        {
            get { return this.compartirHabilitado; }
            set { SetProperty(ref compartirHabilitado, value); }
        }
        private bool compartirHabilitado;

        public bool AgregarVisible
        {
            get { return this.agregarVisible; }
            set { SetProperty(ref agregarVisible, value); }
        }
        private bool agregarVisible;

        #endregion

        #region Commands

        public Command AddCommand { get; set; }

        public Command LoadImageCommand { get; set; }

        public Command ImagePopupCommand { get; set; }

        public Command OpenShareCommand { get; set; }

        public Command CompartirArchivosCommand { get; set; }

        public Command AgregarPopupCommand { get; set; }

        #endregion

        #region Methods

        private async void AgregarRegistro()
        {
            if (mockSuscriptionError)
            {
                ErrorSuscripcion = "Mocked: Suscription error";
                EnlaceSuscripcion = "https://docnoc.mx";
                ErrorSuscripcionVisible = true;
                return;
            }

            var filtro = new ParaFiltroUsuarioyDato()
            {
                IdUsuario = idUsuario,
                Dato = "EstudiosyAnalisisAgregarArchivo"
            };

            var resultadoApi = await DocNocApi.OpenPay.PreValidacionPaciente(filtro);

            if (resultadoApi.Error)
            {
                string[] errorArray = resultadoApi.Mensajes[0].Contenido.Split('|');

                if (errorArray.Length == 3)
                {
                    ErrorSuscripcion = errorArray[1];
                    EnlaceSuscripcion = errorArray[2];
                    ErrorSuscripcionVisible = true;
                    return;
                }

                ErrorEntidad(resultadoApi);

                return;
            }

            AgregarVisible = true;
        }

        private async void AbrirCompartir()
        {
            if (mockSuscriptionError)
            {
                ErrorSuscripcion = "Mocked: Suscription error";
                EnlaceSuscripcion = "https://docnoc.mx";
                ErrorSuscripcionVisible = true;
                return;
            }

            var filtro = new ParaFiltroUsuarioyDato()
            {
                IdUsuario = idUsuario,
                Dato = "EstudiosyAnalisisCompartirArchivo"
            };

            var resultadoApi = await DocNocApi.OpenPay.PreValidacionPaciente(filtro);

            if (resultadoApi.Error)
            {
                string[] errorArray = resultadoApi.Mensajes[0].Contenido.Split('|');

                if (errorArray.Length == 3)
                {
                    ErrorSuscripcion = errorArray[1];
                    EnlaceSuscripcion = errorArray[2];
                    ErrorSuscripcionVisible = true;
                    return;
                }

                ErrorEntidad(resultadoApi);

                return;
            }

            int cuenta = 0;

            foreach (var documento in Registros)
            {
                if (documento.Compartir)
                    cuenta++;
            }

            if(cuenta == 0)
            {
                MensajeError("No se seleccionó ningún archivo para compartir.");
                return;
            }

            InformacionCompartir = $"Se compartirán {cuenta} archivos.";

            CompartirVisible = true;
        }

        private void AbrirImagen(string rutaImagen)
        {
            FuenteImagen = ImageSource.FromUri(new Uri(rutaImagen));

            ImagenVisible = true;
        }

        private async void CargarDatos()
        {
            FechaEvento = DateTime.Now.Date;
            Tipo = null;
            Descripcion = null;
            ImagenVisible = false;

            TiposEvento = new List<TiposEventos>()
            {
                new TiposEventos(){ TipoEvento = "Imagenología" },
                new TiposEventos(){ TipoEvento = "Análisis Clínicos" },
                new TiposEventos(){ TipoEvento = "Otros" }
            };

            var filtroUsuario = new ParaFiltroUsuario2() { IdUsuario = idUsuarioExpediente };

            if(filtroUsuario.IdUsuario != idUsuario)
            {
                filtroUsuario.IdUsuarioPrincipal = idUsuario;
            }

            var resultadoApi = await DocNocApi.Pacientes.ListaEstudiosyAnalisisPaciente(filtroUsuario);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            Registros = new List<EstudioAnalisisPaciente>(resultadoApi.Registros);
        }

        private async void CargarImagenArchivo()
        {
            var img = await CargarImagen("ImagenEstudioAnalisis");

            if (img is null)
                return;

            if(img.Error)
            {
                MensajeError(img.CadenaErrores());
                return;
            }

            Imagen = img;
        }

        private async void CompartirArchivos()
        {
            int porcompartir = 0;
            int compartidos = 0;
            int errores = 0;

            foreach (var documento in Registros)
            {
                if (documento.Compartir)
                {
                    porcompartir++;

                    var archivo = new ParaComparteArchivoExpediente()
                    {
                        IdUsuarioEnvia = idUsuarioExpediente,
                        IdUsuarioRecibe = idUsuarioMedico,
                        IdExpedienteImagen = documento.IdExpedienteImagen,
                        CompartidoHasta = CompartirHasta
                    };

                    var resultadoApi = await DocNocApi.Pacientes.ComparteArchivoExpediente(archivo);

                    if (resultadoApi.Error)
                    {
                        ErrorEntidad(resultadoApi);
                        errores++;
                    }
                    else
                    {
                        compartidos++;
                    }
                }
            }

            string msj = $"Se compartieron {compartidos} de {porcompartir} archivos.";

            if (errores > 0)
                msj += $" {errores} archivos con error.";

            await Dialog.Show("Información", msj, "Aceptar");
        }

        private async void InsertarEvento()
        {
            if (string.IsNullOrWhiteSpace(Descripcion))
            {
                MensajeError("Introduzca la descripción del evento.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Imagen.ImagenBase64))
            {
                MensajeError("Debe cargar un archivo de imagen.");
                return;
            }

            var registro = new AgregaArchivosExpediente()
            {
                IdUsuarioPaciente = idUsuarioExpediente,
                IdUsuarioEnvia = idUsuario,
                Fecha = FechaEvento,
                Tipo = Tipo.TipoEvento,
                Descripcion = Descripcion,
                RutaImagen = Imagen.ImagenBase64,
                ExtensionImagen = Imagen.ExtensionImagen
            };

            var resultadoApi = await DocNocApi.Pacientes.AgregaArchivoExpediente(registro);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            FechaEvento = DateTime.Now;
            Descripcion = string.Empty;
            Imagen = new ImagenInput();

            CargarDatos();
        }

        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}

