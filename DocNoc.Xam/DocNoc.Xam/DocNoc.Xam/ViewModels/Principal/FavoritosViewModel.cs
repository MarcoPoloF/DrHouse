using DocNoc.Models;
using DocNoc.Xam.Enum.Sort;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DocNoc.Xam.ViewModels.Principal
{
    /// <summary>
    /// Definición de ViewModel: Favoritos (dn-56-3).
    /// </summary>
    public class FavoritosViewModel : DocNocViewModel
    {
        #region Constructor

        public FavoritosViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.SortCommand = new Command(ActualizarOrdenamiento);
            this.AgendarCitaCommand = new Command<ListaFavoritos>(NavegarAgendarCita);
            this.PerfilMedicoCommand = new Command<ListaFavoritos>(NavegarPerfilMedico);
            this.RemoverFavoritoCommand = new Command<ListaFavoritos>(RemoverFavorito);
            this.ShareCommand = new Command<ListaFavoritos>(CompartirPerfil);

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

        public List<ListaFavoritos> Medicos
        {
            get { return this.medicos; }
            set { SetProperty(ref medicos, value); }
        }
        private List<ListaFavoritos> medicos;

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
        public Command RemoverFavoritoCommand { get; set; }
        public Command ShareCommand { get; set; }

        #endregion

        #region Methods

        private void ActualizarOrdenamiento()
        {
            OrdenarMedicos(new List<ListaFavoritos>(Medicos));
        }

        private async void CargarDatos()
        {
            IsBusy = true;

            Medicos = new List<ListaFavoritos>();
            MedicosDisponibles = 0;

            //var busqueda = DeserializarJson<BusquedaMedicoAPP>(Preferences.Get("BuscarMedico_Filtro"));

            var resultadoApi = await DocNocApi.Pacientes.ListaFavoritos(new ParaFiltroUsuario() { IdUsuario = idUsuario });

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

        private void OrdenarMedicos(List<ListaFavoritos> registros)
        {

            Medicos = new List<ListaFavoritos>(registros);
            return;

            //if (registros == null || registros.Count < 2)
            //{
            //    Medicos = new List<ListaFavoritos>(registros);
            //    return;
            //}

            //int sortingOption = 0;

            //if (SortRatingSeleccionado)
            //    sortingOption = 1;

            //if (SortApellidoSeleccionado)
            //    sortingOption = 2;

            //switch (sortingOption)
            //{
            //    //Cita Próxima
            //    case 0:
            //        registros.Sort((p, q) => p.ProximaCita.CompareTo(q.ProximaCita));
            //        break;
            //    //Rating
            //    case 1:
            //        registros.Sort((p, q) => q.Rating.CompareTo(p.Rating));
            //        break;
            //    //Apellido
            //    case 2:
            //        registros.Sort((p, q) => p.Apellidos.CompareTo(q.Apellidos));
            //        break;
            //}

            //Medicos = new List<ListaFavoritos>(registros);
        }

        private void NavegarAgendarCita(ListaFavoritos medico)
        {
            Preferences.Set("PerfilMedico_Id", medico.IdUsuarioDoctor);

            //Navegación a página "Agendar Cita" (dn-19-3).
            Navigation.NavigateTo(typeof(ViewModels.Citas.MyAddressViewModel), string.Empty, string.Empty);
        }

        private void NavegarPerfilMedico(ListaFavoritos medico)
        {
            Preferences.Set("PerfilMedico_Id", medico.IdUsuarioDoctor);

            Navigation.NavigateTo(typeof(ViewModels.Medicos.ContactProfileViewModel), string.Empty, string.Empty);
        }

        private async void RemoverFavorito(ListaFavoritos listaFavoritos)
        {
            var resultadoApi = await DocNocApi.Pacientes.EliminaFavorito(new Favorito() { IdUsuario = idUsuario, IdUsuarioDoctor = listaFavoritos.IdUsuarioDoctor });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            CargarDatos();
        }

        private async void CompartirPerfil(ListaFavoritos listaFavoritos)
        {
            await Share.RequestAsync(new ShareTextRequest()
            {
                //Uri = listaFavoritos.RutaPerfil,
                Text = listaFavoritos.RutaPerfil,
                Title = $"Doc+Noc: {listaFavoritos.NombreCompletoTitulo}"
            });
        }


        public void OnAppearing()
        {
            CargaInicial = true;
            CargarDatos();
        }

        #endregion
    }
}

