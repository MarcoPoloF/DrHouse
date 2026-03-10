using DocNoc.Models;
using DocNoc.Xam.Enum.Sort;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Medicos
{
    /// <summary>
    /// Definición de ViewModel: Lista de Médicos (dn-14-3).
    /// </summary>
    public class ListaMedicosViewModel : DocNocViewModel
    {
        #region Constructor

        public ListaMedicosViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.RefreshCommand = new Command(CargarDatos);
            this.SortCommand = new Command(ActualizarOrdenamiento);
            this.AgendarCitaCommand = new Command<ResultadoBusquedaMedicoAPP>(NavegarAgendarCita);
            this.PerfilMedicoCommand = new Command<ResultadoBusquedaMedicoAPP>(NavegarPerfilMedico);

            MedicosDisponibles = 0;

            SortCitaSeleccionado = true;
            SortRatingSeleccionado = false;
            SortApellidoSeleccionado = false;

            IsBusy = true;
            CargaInicial = false;
        }

        #endregion

        #region Fields

        private DoctorSorting Ordenamiento { get; set; }

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

        public List<ResultadoBusquedaMedicoAPP> Medicos
        {
            get { return this.medicos; }
            set { SetProperty(ref medicos, value); }
        }
        private List<ResultadoBusquedaMedicoAPP> medicos;

        public int MedicosDisponibles
        {
            get { return this.medicosDisponibles; }
            set { SetProperty(ref medicosDisponibles, value); }
        }
        private int medicosDisponibles;

        public bool CargaInicial
        {
            get { return this.cargaInicial; }
            set { SetProperty(ref cargaInicial, value); }
        }
        private bool cargaInicial;

        public bool SortCitaSeleccionado
        {
            get { return this.sortCitaSeleccionado; }
            set { SetProperty(ref sortCitaSeleccionado, value); }
        }
        private bool sortCitaSeleccionado;

        public bool SortRatingSeleccionado
        {
            get { return this.sortRatingSeleccionado; }
            set { SetProperty(ref sortRatingSeleccionado, value); }
        }
        private bool sortRatingSeleccionado;

        public bool SortApellidoSeleccionado
        {
            get { return this.sortApellidoSeleccionado; }
            set { SetProperty(ref sortApellidoSeleccionado, value); }
        }
        private bool sortApellidoSeleccionado;

        #endregion

        #region Commands

        public Command RefreshCommand { get; set; }

        public Command SortCommand { get; set; }

        public Command AgendarCitaCommand { get; set; }

        public Command PerfilMedicoCommand { get; set; }

        #endregion

        #region Methods

        private void ActualizarOrdenamiento()
        {
            OrdenarMedicos(new List<ResultadoBusquedaMedicoAPP>(Medicos));
        }

        private async void CargarDatos()
        {
            IsBusy = true;

            Medicos = new List<ResultadoBusquedaMedicoAPP>();
            MedicosDisponibles = 0;

            var busqueda = DeserializarJson<BusquedaMedicoAPP>(Preferences.Get("BuscarMedico_Filtro"));

            var resultadoApi = await DocNocApi.Citas.BuscaMedico2(busqueda);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                IsBusy = false;
                CargaInicial = false;
                return;
            }

            OrdenarMedicos(resultadoApi.Registros);

            MedicosDisponibles = Medicos.Count;

            IsBusy = false;
            CargaInicial = false;
        }

        private void OrdenarMedicos(List<ResultadoBusquedaMedicoAPP> registros)
        {
            if(registros == null || registros.Count < 2)
            {
                Medicos = new List<ResultadoBusquedaMedicoAPP>(registros);
                return;
            }

            int sortingOption = 0;

            if (SortRatingSeleccionado)
                sortingOption = 1;

            if (SortApellidoSeleccionado)
                sortingOption = 2;

            switch (sortingOption)
            {
                //Cita Próxima
                case 0:
                    registros.Sort((p, q) => p.ProximaCita.CompareTo(q.ProximaCita));
                    break;
                //Rating
                case 1:
                    registros.Sort((p, q) => q.Rating.CompareTo(p.Rating));
                    break;
                //Apellido
                case 2:
                    registros.Sort((p, q) => p.Apellidos.CompareTo(q.Apellidos));
                    break;
            }

            Medicos = new List<ResultadoBusquedaMedicoAPP>(registros);
        }

        private void NavegarAgendarCita(ResultadoBusquedaMedicoAPP medico)
        {
            Preferences.Set("PerfilMedico_Id", medico.IdUsuario);

            //Navegación a página "Agendar Cita" (dn-19-3).
            Navigation.NavigateTo(typeof(ViewModels.Citas.MyAddressViewModel), string.Empty, string.Empty);
        }

        private void NavegarPerfilMedico(ResultadoBusquedaMedicoAPP medico)
        {
            Preferences.Set("PerfilMedico_Id", medico.IdUsuario);

            Navigation.NavigateTo(typeof(ViewModels.Medicos.ContactProfileViewModel), string.Empty, string.Empty);
        }

        public void OnAppearing()
        {
            if(Medicos == null)
            {
                CargaInicial = true;
                CargarDatos();
            }
        }

        #endregion
    }
}

