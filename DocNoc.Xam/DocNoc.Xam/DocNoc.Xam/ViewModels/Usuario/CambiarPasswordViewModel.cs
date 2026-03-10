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
    /// Definición de ViewModel: Cambiar Contraseña (dn-61-3).
    /// </summary>
    public class CambiarPasswordViewModel : DocNocViewModel
    {
        #region Constructor

        public CambiarPasswordViewModel (INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.CancelCommand = new Command(CancelarCambios);
            this.UpdateCommand = new Command(CambiarPassword);
            this.ResetPasswordCommand = new Command(IrRestaurarPassword);
            this.CheckChangesCommand = new Command(ValidarCambios);
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

        public string PasswordActual
        {
            get { return this.passwordActual; }
            set { SetProperty(ref passwordActual, value); }
        }
        private string passwordActual;

        public string PasswordNuevo
        {
            get { return this.passwordNuevo; }
            set { SetProperty(ref passwordNuevo, value); }
        }
        private string passwordNuevo;

        public string PasswordConfirmacion
        {
            get { return this.passwordConfirmacion; }
            set { SetProperty(ref passwordConfirmacion, value); }
        }
        private string passwordConfirmacion;

        public bool DatosValidos
        {
            get { return this.datosValidos; }
            set { SetProperty(ref datosValidos, value); }
        }
        private bool datosValidos;

        public bool HayDatos
        {
            get { return this.hayDatos; }
            set { SetProperty(ref hayDatos, value); }
        }
        private bool hayDatos;

        public bool PasswordsDistintos
        {
            get { return this.passwordsDistintos; }
            set { SetProperty(ref passwordsDistintos, value); }
        }
        private bool passwordsDistintos;

        #endregion

        #region Commands

        public Command CancelCommand { get; set; }

        public Command UpdateCommand { get; set; }

        public Command ResetPasswordCommand { get; set; }

        public Command CheckChangesCommand { get; set; }

        #endregion

        #region Methods

        private async void CambiarPassword()
        {
            IsBusy = true;

            var respuestaApi = await DocNocApi.Usuarios.CambiaContrasenia(new NuevaContrasenia()
            {
                IdUsuario = idUsuario,
                Contrasenia = PasswordNuevo,
                ContraseniaAnterior = PasswordActual
            });

            if (respuestaApi.Error)
            {
                ErrorEntidad(respuestaApi);
            }
            else
            {
                await Dialog.Show("Éxito", "Se ha actualizado tu contraseña.", "Aceptar");
            }

            IsBusy = false;

            CancelarCambios();
        }

        private void CancelarCambios()
        {
            PasswordActual = string.Empty;
            PasswordNuevo = string.Empty;
            PasswordConfirmacion = string.Empty;
        }

        private void IrRestaurarPassword()
        {
            //Navegación a página "Recuperar Contraseña" (dn-06-3).
            Navigation.NavigateTo(typeof(ViewModels.Acceso.ForgotPasswordViewModel), string.Empty, string.Empty);
        }

        private void ValidarCambios()
        {
            HayDatos = false;
            DatosValidos = false;
            PasswordsDistintos = false;

            int i = 0;

            if (!string.IsNullOrWhiteSpace(PasswordActual))
            {
                HayDatos = true;
                i++;
            }                

            if (!string.IsNullOrWhiteSpace(PasswordNuevo))
            {
                HayDatos = true;
                i++;
            }

            if (!string.IsNullOrWhiteSpace(PasswordConfirmacion))
            {
                HayDatos = true;
                i++;
            }

            if(i == 3)
            {
                if (PasswordNuevo == PasswordConfirmacion)
                    DatosValidos = true;
                else
                    PasswordsDistintos = true;
            }
        }

        public void OnAppearing()
        {
            ValidarCambios();
        }

        #endregion
    }
}

