using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Expedientes
{
    /// <summary>
    /// Definición de ViewModel: Antecedentes Familiares (dn-43-3).
    /// </summary>
    public class AntecedentesFamiliaresViewModel : DocNocViewModel
    {
        #region Constructor

        public AntecedentesFamiliaresViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.AddCommand = new Command(InsertarAntecedente);

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

        public List<UsuarioAntecedenteFamiliarPatologico> Antecedentes
        {
            get { return this.antecedentes; }
            set { SetProperty(ref antecedentes, value); }
        }
        private List<UsuarioAntecedenteFamiliarPatologico> antecedentes;

        public string Padecimiento
        {
            get { return this.padecimiento; }
            set { SetProperty(ref padecimiento, value); }
        }
        private string padecimiento;

        public string Parentesco
        {
            get { return this.parentesco; }
            set { SetProperty(ref parentesco, value); }
        }
        private string parentesco;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command AddCommand { get; set; }

        #endregion

        #region Methods

        private async void InsertarAntecedente()
        {
            if (string.IsNullOrWhiteSpace(Padecimiento))
            {
                MensajeError("Introduzca el nombre del padecimiento.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Parentesco))
            {
                MensajeError("Introduzca la relación familiar.");
                return;
            }

            var registro = new UsuarioAntecedenteFamiliarPatologico()
            {
                IdUsuario = idUsuario,
                AntecedentePatologico = Padecimiento,
                Familiar = Parentesco
            };

            if (EsUsuarioAdicional)
                registro.IdUsuarioAdicional = IdExpediente;

            var resultadoApi = await DocNocApi.Usuarios.InsertaUsuarioAntecedenteFamiliarPatologico(registro);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            Padecimiento = string.Empty;
            Parentesco = string.Empty;

            CargarAntecedentes();
        }

        private async void CargarAntecedentes()
        {
            var resultadoApi = await DocNocApi.Usuarios.TraeUsuarioAntecedenteFamiliarPatologico(new ParaFiltroUsuario() { IdUsuario = IdExpediente });

            if (resultadoApi.Error)
            {
                return;
            }

            Antecedentes = new List<UsuarioAntecedenteFamiliarPatologico>(resultadoApi.Registros);
        }

        public void OnAppearing()
        {
            CargarAntecedentes();
        }

        #endregion
    }
}


