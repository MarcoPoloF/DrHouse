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

namespace DocNoc.Xam.ViewModels.Consultas
{
    /// <summary>
    /// Definición de ViewModel: Historial de Consultas (dn-30-3).
    /// </summary>
    public class HistorialConsultasViewModel : DocNocViewModel
    {
        #region Constructor

        public HistorialConsultasViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Home (dn-07-3).
            //PageText = text.Get<PaginaTxt>("dn-07-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            IsBusy = false;

            this.BackCommand = new Command(Regresar);
            this.RefreshCommand = new Command(CargarConsultas);
            this.ItemSelectedCommand = new Command<object>(NavegarDetalleConsulta);

            ValidarTipoUsuario();
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

        public List<Consulta> Consultas
        {
            get { return this.consultas; }
            set { SetProperty(ref consultas, value); }
        }
        private List<Consulta> consultas;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when the appointments list is refreshed.
        /// </summary>
        public Command RefreshCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when an appointment is tapped.
        /// </summary>
        public Command ItemSelectedCommand { get; set; }

        #endregion

        #region Methods

        private async void CargarConsultas()
        {
            IsBusy = true;

            var respuestaApi = await DocNocApi.Usuarios.TraeHistoriaConsulta(new ParaFiltroUsuario() { IdUsuario = IdExpediente });

            if (respuestaApi.Error)
            {
                ErrorEntidad(respuestaApi);
                return;
            }

            Consultas = new List<Consulta>(respuestaApi.Registros);

            IsBusy = false;
        }

        private void NavegarDetalleConsulta(object obj)
        {
            var consulta = (Consulta)obj;

            Preferences.Set("IdConsulta", consulta.IdConsulta.ToString());

            Navigation.NavigateTo(typeof(ViewModels.Consultas.DetalleConsultaViewModel), string.Empty, string.Empty);
        }

        public void OnAppearing()
        {
            CargarConsultas();
        }

        #endregion
    }
}
