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

namespace DocNoc.Xam.ViewModels.Expedientes
{
    /// <summary>
    /// Definición de ViewModel: Historia Clínica (dn-45-3).
    /// </summary>
    public class HistoriaClinicaViewModel : DocNocViewModel
    {
        #region Constructor

        public HistoriaClinicaViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.AddCommand = new Command(InsertarEvento);
            this.IrSuscripcionCommand = new Command(AbrirSuscripcion);
            this.AgregarPopupCommand = new Command(AgregarRegistro);

            FechaEvento = DateTime.MinValue;

            ValidarTipoUsuario();
        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;

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

        public List<ExpedienteHistoriaClinica> Historia
        {
            get { return this.historia; }
            set { SetProperty(ref historia, value); }
        }
        private List<ExpedienteHistoriaClinica> historia;

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

        public TiposEventos Tipo
        {
            get { return this.tipo; }
            set { SetProperty(ref tipo, value); }
        }
        private TiposEventos tipo;

        public string Motivo
        {
            get { return this.motivo; }
            set { SetProperty(ref motivo, value); }
        }
        private string motivo;

        public string Comentario
        {
            get { return this.comentario; }
            set { SetProperty(ref comentario, value); }
        }
        private string comentario;

        public bool AgregarVisible
        {
            get { return this.agregarVisible; }
            set { SetProperty(ref agregarVisible, value); }
        }
        private bool agregarVisible;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command AddCommand { get; set; }

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
                Dato = "HistoriaClinicaAgregarEvento"
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

        private async void InsertarEvento()
        {
            if (string.IsNullOrWhiteSpace(Motivo))
            {
                MensajeError("Introduzca el motivo del evento.");
                return;
            }

            var registro = new ExpedienteHistoriaClinica()
            {
                IdUsuarioPaciente = idUsuario,
                Fecha = FechaEvento,
                Tipo = Tipo.TipoEvento,
                Motivo = Motivo,
                Comentarios = Comentario
            };

            if (EsUsuarioAdicional)
                registro.IdUsuarioAdicional = IdExpediente;

            var resultadoApi = await DocNocApi.Pacientes.AgregaHistClinicaelPaciente(registro);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            Motivo = string.Empty;
            Comentario = string.Empty;

            CargarDatos();
        }

        private async void CargarDatos()
        {
            FechaEvento = DateTime.Now.Date;
            Tipo = null;
            Motivo = null;
            Comentario = null;

            var catalogoTipos = await DocNocApi.TipoEvento.Lista();

            if (catalogoTipos.Error)
            {
                ErrorEntidad(catalogoTipos);
                return;
            }

            TiposEvento = new List<TiposEventos>(catalogoTipos.Registros);

            var datoUsuario = new ParaFiltroUsuario() { IdUsuario = idUsuario };

            if (EsUsuarioAdicional)
                datoUsuario.IdUsuarioAdicional = IdExpediente;

            var resultadoApi = await DocNocApi.Pacientes.HistClinicoPacienteelPaciente(datoUsuario);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            Historia = new List<ExpedienteHistoriaClinica>(resultadoApi.Registros);
        }

        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}


