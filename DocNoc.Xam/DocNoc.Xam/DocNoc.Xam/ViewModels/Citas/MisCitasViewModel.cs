using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.PullToRefresh;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Citas
{
    /// <summary>
    /// Definición de ViewModel: Mis Citas (dn-26-3).
    /// </summary>
    public class MisCitasViewModel : DocNocViewModel
    {
        #region Constructor

        public MisCitasViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Home (dn-07-3).
            //PageText = text.Get<PaginaTxt>("dn-07-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            IsRefreshing = false;

            this.RefreshCommand = new Command(CargarCitas);
            this.ItemSelectedCommand = new Command<object>(NavegarDetalleCita);
            this.HistorialCommand = new Command(NavegarHistorialConsultas);

            CargarCitas();

        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;

        private List<ResultadoMisCitas> citas;

        private bool isRefreshing;

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

        public List<ResultadoMisCitas> Citas
        {
            get { return this.citas; }
            set { SetProperty(ref citas, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

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

        public Command HistorialCommand { get; set; }

        #endregion

        #region Methods

        private async void CargarCitas()
        {
            IsRefreshing = true;

            var respuestaApi = await DocNocApi.Citas.MisCitas(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (respuestaApi.Error)
            {
                ErrorEntidad(respuestaApi);
                IsRefreshing = false;
                return;
            }

            //Se ordenan las citas en de más próxima a más lejana.
            respuestaApi.Registros.Sort((p, q) => p.FechaCitaI.CompareTo(q.FechaCitaI));

            DateTime fechaComparacion = DateTime.MinValue;

            foreach(var cita in respuestaApi.Registros)
            {
                if(cita.IdCita != respuestaApi.Registros.First().IdCita)
                {
                    if (cita.FechaCitaI.Date != fechaComparacion.Date)
                    {
                        cita.MostrarDia = true;
                    }
                    else
                    {
                        cita.MostrarDia = false;
                    }
                }
                else
                {
                    cita.MostrarDia = true;
                }

                fechaComparacion = cita.FechaCitaI.Date;
            }

            Citas = new List<ResultadoMisCitas>(respuestaApi.Registros);

            IsRefreshing = false;
        }

        private void NavegarDetalleCita(object obj)
        {
            var cita = (ResultadoMisCitas)obj;

            Preferences.Set("IdCita", cita.IdCita.ToString());

            Navigation.NavigateTo(typeof(ViewModels.Citas.DetalleCitaViewModel), string.Empty, string.Empty);
        }

        private void NavegarHistorialConsultas()
        {
            Navigation.NavigateTo(typeof(ViewModels.Consultas.HistorialConsultasViewModel), string.Empty, string.Empty);
        }

        public void OnAppearing()
        {
            CargarCitas();
        }

        #endregion
    }
}
