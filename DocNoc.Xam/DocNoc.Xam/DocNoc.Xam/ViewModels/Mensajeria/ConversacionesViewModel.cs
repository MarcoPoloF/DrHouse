using DocNoc.Models;
using DocNoc.Xam.Custom.Maps;
using DocNoc.Xam.Enum.Sort;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DocNoc.Xam.ViewModels.Mensajeria
{
    /// <summary>
    /// Definición de ViewModel: Mapa de Médicos (dn-13-3).
    /// </summary>
    public class ConversacionesViewModel : DocNocViewModel
    {
        #region Constructor

        public ConversacionesViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.IrChatCommand = new Command<ParaPaginaMensajeAPP>(AbrirChat);
        }

        #endregion

        #region Properties

        public List<ParaPaginaMensajeAPP> Conversaciones
        {
            get { return this.conversaciones; }
            set { SetProperty(ref conversaciones, value); }
        }
        private List<ParaPaginaMensajeAPP> conversaciones;

        #endregion

        #region Commands

        public Command IrChatCommand { get; set; }

        #endregion

        #region Methods

        private async void CargarDatos()
        {
            var resultadoApi = await DocNocApi.Mensajes.PaginaMensajeAPP(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            Conversaciones = new List<ParaPaginaMensajeAPP>(resultadoApi.Registros);
        }

        private void AbrirChat(ParaPaginaMensajeAPP medico)
        {
            Preferences.Set("IdUsuario_Chat", medico.IdUsuario);

            //Navegación a página "Chat" (dn-71-3).
            Navigation.NavigateTo(typeof(ViewModels.Mensajeria.ChatViewModel), string.Empty, string.Empty);
        }

        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}



