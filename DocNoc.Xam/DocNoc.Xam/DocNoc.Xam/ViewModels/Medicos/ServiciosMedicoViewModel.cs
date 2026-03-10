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

namespace DocNoc.Xam.ViewModels.Medicos
{
    /// <summary>
    /// Definición de ViewModel: Servicios de Médico (dn-18-3).
    /// </summary>
    public class ServiciosMedicoViewModel : DocNocViewModel
    {
        #region Constructor

        public ServiciosMedicoViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;

        private string idMedico;

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

        public List<UsuarioServicioTratamiento> Servicios
        {
            get { return this.servicios; }
            set { SetProperty(ref servicios, value); }
        }
        private List<UsuarioServicioTratamiento> servicios;

        #endregion

        #region Methods

        private async void CargarServicios()
        {
            var resultadoApi = await DocNocApi.Usuarios.ListaUsuarioServicioTratamiento(new ParaFiltroUsuario() { IdUsuario = idMedico });

            if (resultadoApi.Error)
            {
                return;
            }

            Servicios = new List<UsuarioServicioTratamiento>(resultadoApi.Registros);
        }

        public void OnAppearing()
        {
            idMedico = Preferences.Get("IdMedico_Servicios");
            CargarServicios();
        }

        #endregion
    }
}


