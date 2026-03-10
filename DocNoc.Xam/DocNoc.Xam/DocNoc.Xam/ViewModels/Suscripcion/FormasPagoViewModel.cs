using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using Openpay.Xamarin;
using PPS.Estandar;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Openpay;
using Openpay.Xamarin.Abstractions;

namespace DocNoc.Xam.ViewModels.Suscripcion
{
    /// <summary>
    /// Definición de ViewModel: Formas de Pago (dn-62-3).
    /// </summary>
    public class FormasPagoViewModel : DocNocViewModel
    {
        #region Constructor

        public FormasPagoViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.AddCommand = new Command(InsertarFormaPago);

            IsBusy = false;
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

        public List<UsuarioPago> FormasPago
        {
            get { return this.formasPago; }
            set { SetProperty(ref formasPago, value); }
        }
        private List<UsuarioPago> formasPago;

        public AltaTarjetaParaPaciente NuevaTarjeta
        {
            get { return this.nuevaTarjeta; }
            set { SetProperty(ref nuevaTarjeta, value); }
        }
        private AltaTarjetaParaPaciente nuevaTarjeta;

        #endregion

        #region Commands

        public Command AddCommand { get; set; }

        #endregion

        #region Methods

        private async void CargarFormasPago()
        {
            var resultadoApi = await DocNocApi.Usuarios.TraeUsuarioPago(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (resultadoApi.Error)
            {
                return;
            }

            FormasPago = new List<UsuarioPago>(resultadoApi.Registros);

            NuevaTarjeta = new AltaTarjetaParaPaciente()
            {
                IdUsuario = idUsuario
            };
        }

        private async void InsertarFormaPago()
        {

            if (!CrossOpenpay.IsSupported)
            {
                MensajeError("Esta plataforma no soporta OpenPay.");
                return;
            }

            IsBusy = true;

            /*var deviceSessionId = await CrossOpenpay.Current.CreateDeviceSessionId();

            NuevaTarjeta.DeviceSessionId = deviceSessionId;*/

            if(string.IsNullOrWhiteSpace(NuevaTarjeta.CardNumber))
            {
                MensajeError("Debe proporcionar el número de la tarjeta.");
                IsBusy = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevaTarjeta.HolderName))
            {
                MensajeError("Debe proporcionar el nombre del titular.");
                IsBusy = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevaTarjeta.ExpirationMonth))
            {
                MensajeError("Debe proporcionar la fecha de expiración.");
                IsBusy = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevaTarjeta.ExpirationYear))
            {
                MensajeError("Debe proporcionar la fecha de expiración.");
                IsBusy = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(NuevaTarjeta.Cvv2))
            {
                MensajeError("Debe proporcionar el código de seguridad.");
                IsBusy = false;
                return;
            }

            Card card = new Card
            {
                HolderName = NuevaTarjeta.HolderName,
                Number = NuevaTarjeta.CardNumber,
                ExpirationMonth = NuevaTarjeta.ExpirationMonth,
                ExpirationYear = NuevaTarjeta.ExpirationYear,
                Cvv2 = Int16.Parse(NuevaTarjeta.Cvv2)
            };

            var token = await CrossOpenpay.Current.CreateTokenFromCard(card);
            var deviceSessionId = await CrossOpenpay.Current.CreateDeviceSessionId();

            NuevaTarjeta.DeviceSessionId = deviceSessionId;
            NuevaTarjeta.TokenID = token.Id;


            var resultadoApi = await DocNocApi.OpenPay.AltaTarjeta(NuevaTarjeta);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                IsBusy = false;
                return;
            }

            IsBusy = false;

            CargarFormasPago();
        }

        public void OnAppearing()
        {
            CargarFormasPago();
        }

        #endregion
    }
}


