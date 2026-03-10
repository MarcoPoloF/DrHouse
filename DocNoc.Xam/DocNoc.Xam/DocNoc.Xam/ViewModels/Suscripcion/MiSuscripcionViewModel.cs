using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using Openpay.Xamarin;
using PPS.Estandar;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DocNoc.Xam.ViewModels.Suscripcion
{
    /// <summary>
    /// Definición de ViewModel: Mi Suscripción (dn-64-3).
    /// </summary>
    public class MiSuscripcionViewModel : DocNocViewModel
    {
        #region Constructor

        public MiSuscripcionViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.RefreshCommand = new Command(CargarDatos);
            this.BuyPopupCommand = new Command(AbrirComprar);
            this.BuyCommand = new Command(ComprarMembresia);
            this.FormasPagoCommand = new Command(IrFormasPago);
            ComprarAbierto = false;
        }

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
        //private PaginaTxt pageText;

        public List<SuscripcionPaciente> Planes
        {
            get { return this.planes; }
            set { SetProperty(ref planes, value); }
        }
        private List<SuscripcionPaciente> planes;

        public SuscripcionPaciente PlanSeleccionado
        {
            get { return this.planSeleccionado; }
            set { SetProperty(ref planSeleccionado, value); }
        }
        private SuscripcionPaciente planSeleccionado;

        public List<UsuarioPago> FormasPago
        {
            get { return this.formasPago; }
            set { SetProperty(ref formasPago, value); }
        }
        private List<UsuarioPago> formasPago;

        public UsuarioPago TarjetaSeleccionada
        {
            get { return this.tarjetaSeleccionada; }
            set { SetProperty(ref tarjetaSeleccionada, value); }
        }
        private UsuarioPago tarjetaSeleccionada;

        public MiSuscripcion Suscripcion
        {
            get { return this.suscripcion; }
            set { SetProperty(ref suscripcion, value); }
        }
        private MiSuscripcion suscripcion;

        public Usuario Usuario
        {
            get { return this.usuario; }
            set { SetProperty(ref usuario, value); }
        }
        private Usuario usuario;

        
        public bool ComprarAbierto
        {
            get { return this.comprarAbierto; }
            set { SetProperty(ref comprarAbierto, value); }
        }
        private bool comprarAbierto;


        public bool ComprandoSuscripcion
        {
            get { return this._comprandoSuscripcion; }
            set { SetProperty(ref _comprandoSuscripcion, value); }
        }
        private bool _comprandoSuscripcion;

        #endregion

        #region Commands

        public Command BuyCommand { get; set; }

        public Command BuyPopupCommand { get; set; }

        public Command RefreshCommand { get; set; }

        public Command SelectPlanCommand { get; set; }

        public Command FormasPagoCommand { get; set; }

        #endregion

        #region Methods

        private void AbrirComprar()
        {
            if(FormasPago.Count == 0)
            {
                Navigation.NavigateTo(typeof(ViewModels.Suscripcion.FormasPagoViewModel), string.Empty, string.Empty);
            }

            ComprarAbierto = true;
        }

        private async void CargarDatos()
        {
            IsBusy = true;

            var resultadoApiSuscripcion = await DocNocApi.Suscripciones.MiSuscripcion(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (resultadoApiSuscripcion.Error)
            {
                ErrorEntidad(resultadoApiSuscripcion);
                IsBusy = false;
                return;
            }

            if (resultadoApiSuscripcion.TieneRegistros)
            {
                Suscripcion = resultadoApiSuscripcion.Registros.Find(x => x.Activa == true);
            }
            else
            {
                Suscripcion = null;
            }

            var resultadoApiTarjetas = await DocNocApi.Usuarios.TraeUsuarioPago(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (resultadoApiTarjetas.Error)
            {
                ErrorEntidad(resultadoApiTarjetas);
                IsBusy = false;
                return;
            }

            FormasPago = new List<UsuarioPago>(resultadoApiTarjetas.Registros);

            var respuestaApiUsuario = await DocNocApi.Usuarios.TraeUsuario(new ParaFiltroUsuario() { IdUsuario = IdExpediente });

            if (respuestaApiUsuario.Error)
            {
                ErrorEntidad(respuestaApiUsuario);
                IsBusy = false;
                return;
            }

            Usuario = respuestaApiUsuario.Contenido;

            var resultadoApiPlanes = await DocNocApi.Suscripciones.ListadoSuscripcionParaPaciente();

            if (resultadoApiPlanes.Error)
            {
                ErrorEntidad(resultadoApiPlanes);
                IsBusy = false;
                return;
            }

            if(Suscripcion != null)
            {
                foreach (var plan in resultadoApiPlanes.Registros)
                {
                    if (plan.IdPlanOpenPay == Suscripcion.IdPlanOpenPay)
                    {
                        plan.Activa = true;
                    }
                }
            }

            Planes = new List<SuscripcionPaciente>(resultadoApiPlanes.Registros);

            IsBusy = false;
        }

        private async void ComprarMembresia()
        {
            if (PlanSeleccionado == null)
            {
                MensajeError("No se ha seleccionado una suscripción");
                return;
            }

            var idSuscripcion = PlanSeleccionado.IdSuscripcion;
            var idOpenpay = PlanSeleccionado.IdPlanOpenPay;

            Planes = new List<SuscripcionPaciente>();

            ComprandoSuscripcion = true;

            var resultadoCompra = await ProcesarCompraMembresia(idSuscripcion, idOpenpay);

            if (resultadoCompra.Error)
                MensajeError(resultadoCompra.CadenaErrores());
            else
                await Dialog.Show("Éxito", "Se ha adquirido la suscripción", "Aceptar");

            ComprandoSuscripcion = false;

            CargarDatos();
        }

        private async Task<Entidad> ProcesarCompraMembresia(int idSuscripcion, string idOpenpay)
        {
            if (Suscripcion != null && Suscripcion.IdPlanOpenPay == idOpenpay)
                return new Entidad("Éste plan está activo actualmente.", true);

            if (TarjetaSeleccionada == null)
                return new Entidad("No se ha seleccionado una tarjeta.", true);

            if (Usuario == null)
                return new Entidad("No se pudo acceder a la información del usuario.", true);

            if (Usuario.IdUsuarioOpenPay == null)
                return new Entidad("El usuario no cuenta con un ID de OpenPay.", true);

            if (!CrossOpenpay.IsSupported)
                return new Entidad("Esta plataforma no soporta OpenPay.", true);

            var nuevaSuscripcion = new CompraSuscripcionPaciente()
            {
                IdUsuario = idUsuario,
                IdPlanOpenPay = idOpenpay,
                IdPagoOpenPay = TarjetaSeleccionada.IdPagoOpenPay,
                IdSuscripcion = idSuscripcion,
                IdUsuarioOpenPay = Usuario.IdUsuarioOpenPay
            };

            var resultadoApi = await DocNocApi.OpenPay.ComprarSuscripcion(nuevaSuscripcion);

            if (resultadoApi.Error)
                return new Entidad(resultadoApi.Mensajes);

            return new Entidad();
        }

        private void IrFormasPago()
        {
            Navigation.NavigateTo(typeof(ViewModels.Suscripcion.FormasPagoViewModel), string.Empty, string.Empty);
        }


        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}


