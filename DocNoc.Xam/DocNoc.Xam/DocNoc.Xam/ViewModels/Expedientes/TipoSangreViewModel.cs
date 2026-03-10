using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DocNoc.Xam.ViewModels.Expedientes
{
    /// <summary>
    /// Definición de ViewModel: Tipo de Sangre (dn-40-3).
    /// </summary>
    public class TipoSangreViewModel : DocNocViewModel
    {
        #region Constructor

        public TipoSangreViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.EditCommand = new Command(EditarDatos);

            ValidarTipoUsuario();

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

        public List<string> TiposSangre
        {
            get { return this.tiposSangre; }
            set { SetProperty(ref tiposSangre, value); }
        }
        private List<string> tiposSangre;

        public string TipoSangre
        {
            get { return this.tipoSangre; }
            set { SetProperty(ref tipoSangre, value); }
        }
        private string tipoSangre;

        public string TipoSangreOriginal
        {
            get { return this.tipoSangreOriginal; }
            set { SetProperty(ref tipoSangreOriginal, value); }
        }
        private string tipoSangreOriginal;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command EditCommand { get; set; }

        #endregion

        #region Methods

        private async void CargarDatos()
        {
            TiposSangre = new List<string>()
            {
                "O-","O+", "A-","A+","B-","B+","AB-","AB+"
            };

            var resultadoApi = await DocNocApi.Usuarios.TraeTipodeSangre(new ParaFiltroUsuario() { IdUsuario = IdExpediente });

            if (resultadoApi.Error)
            {
                MensajeError(resultadoApi.CadenaErrores());
                return;
            }

            if(resultadoApi.Registros.Count == 0)
            {
                MensajeError("No se recuperaron registros");
                return;
            }

            if (resultadoApi.Registros.First().TipoSangre != null 
                && TiposSangre.Any(str => str.Contains(resultadoApi.Registros.First().TipoSangre)))
            {
                TipoSangre = resultadoApi.Registros.First().TipoSangre;
                TipoSangreOriginal = resultadoApi.Registros.First().TipoSangre;
            }
            else
            {
                TipoSangre = null;
                TipoSangreOriginal = null;
            }
        }

        private async void EditarDatos()
        {
            if (TipoSangre == null)
            {
                return;
            }

            if(!string.IsNullOrWhiteSpace(TipoSangreOriginal) && TipoSangreOriginal == TipoSangre)
            {
                return;
            }

            var registro = new TipoDeSangre()
            {
                IdUsuario = IdExpediente,
                TipoSangre = TipoSangre
            };

            var resultadoApi = await DocNocApi.Usuarios.ActualizaTipodeSangre(registro);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                CargarDatos();
                return;
            }
        }

        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}
