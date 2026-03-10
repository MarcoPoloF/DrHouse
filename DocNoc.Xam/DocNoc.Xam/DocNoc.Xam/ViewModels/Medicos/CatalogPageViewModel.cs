using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Medicos
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    public class CatalogPageViewModel : DocNocViewModel
    {
        #region Fields

        private Command addFavouriteCommand;

        private Command itemSelectedCommand;

        private Command agendarCitaCommand;

        private Command sortCommand;

        private Command filterCommand;

        private Command addToCartCommand;

        private Command cardItemCommand;

        private string cartItemCount;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CatalogPageViewModel" /> class.
        /// </summary>
        public CatalogPageViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            IsBusy = false;

            this.RefreshCommand = new Command(CargarResultados);
            this.BackCommand = new Command(Regresar);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the item details in tile.
        /// </summary>
        public List<ResultadoBusquedaMedicoAPP> Medicos
        {
            get { return this.medicos; }
            set { SetProperty(ref medicos, value); }
        }
        private List<ResultadoBusquedaMedicoAPP> medicos;

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command to be executed when the appointments list is refreshed.
        /// </summary>
        public Command RefreshCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand
        {
            get { return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command(this.ItemSelected)); }
        }

        
        public Command AgendarCitaCommand
        {
            get { return this.agendarCitaCommand ?? (this.agendarCitaCommand = new Command(this.AgendarCita)); }
        }

        #endregion

        #region Methods

        private void AgendarCita(object attachedObject)
        {
            var medico = (ResultadoBusquedaMedicoAPP)attachedObject;

            Preferences.Set("PerfilMedico_Id", medico.IdUsuario);

            //Navegación a página "Agendar Cita" (dn-19-3).
            Navigation.NavigateTo(typeof(ViewModels.Citas.MyAddressViewModel), string.Empty, string.Empty);
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void ItemSelected(object attachedObject)
        {
            var medico = (ResultadoBusquedaMedicoAPP)attachedObject;

            Preferences.Set("PerfilMedico_Id", medico.IdUsuario);

            Navigation.NavigateTo(typeof(ViewModels.Medicos.ContactProfileViewModel), string.Empty, string.Empty);
        }

        private async void CargarResultados()
        {
            IsBusy = true;

            var busqueda = DeserializarJson<BusquedaMedicoAPP>(Preferences.Get("BuscarMedico_Filtro"));

            var resultadoApi = await DocNocApi.Citas.BuscaMedico2(busqueda);

            if (!resultadoApi.Error)
            {
                Medicos = new List<ResultadoBusquedaMedicoAPP>(resultadoApi.Registros);
            }

            IsBusy = false;
        }

        public void OnAppearing()
        {
            CargarResultados();
        }

        #endregion
    }
}