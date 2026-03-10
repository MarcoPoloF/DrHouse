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
    public class ComentariosMedicoViewModel : DocNocViewModel
    {
        #region Constructor

        public ComentariosMedicoViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.RefreshCommand = new Command(CargarCalificaciones);
            this.SortCommand = new Command(ActualizarOrdenamiento);
            this.DetalleComentarioCommand = new Command<ParaTraeReviewsDelDoctor>(VerDetalleComentario);

            SortAntiguedadSeleccionado = true;

            IsBusy = true;
            CargaInicial = false;
        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;

        private string _idMedico;

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

        public ParaTraeReviewsDelDoctor Comentario
        {
            get { return this._comentario; }
            set { SetProperty(ref _comentario, value); }
        }
        private ParaTraeReviewsDelDoctor _comentario;

        public List<ParaTraeReviewsDelDoctor> Calificaciones
        {
            get { return this._calificaciones; }
            set { SetProperty(ref _calificaciones, value); }
        }
        private List<ParaTraeReviewsDelDoctor> _calificaciones;

        public string TituloPagina
        {
            get { return this._tituloPagina; }
            set { SetProperty(ref _tituloPagina, value); }
        }
        private string _tituloPagina;

        public int TotalOpiniones
        {
            get { return this._totalOpiniones; }
            set { SetProperty(ref _totalOpiniones, value); }
        }
        private int _totalOpiniones;

        public bool DetalleVisible
        {
            get { return this._detalleVisible; }
            set { SetProperty(ref _detalleVisible, value); }
        }
        private bool _detalleVisible;

        public bool CargaInicial
        {
            get { return this.cargaInicial; }
            set { SetProperty(ref cargaInicial, value); }
        }
        private bool cargaInicial;

        public bool SortAntiguedadSeleccionado
        {
            get { return this._sortAntiguedadSeleccionado; }
            set { SetProperty(ref _sortAntiguedadSeleccionado, value); }
        }
        private bool _sortAntiguedadSeleccionado;

        public bool SortRatingSeleccionado
        {
            get { return this._sortRatingSeleccionado; }
            set { SetProperty(ref _sortRatingSeleccionado, value); }
        }
        private bool _sortRatingSeleccionado;

        #endregion

        #region Commands

        public Command RefreshCommand { get; set; }

        public Command SortCommand { get; set; }

        public Command DetalleComentarioCommand { get; set; }

        #endregion

        #region Methods

        private void ActualizarOrdenamiento()
        {
            OrdenarCalificaciones(Calificaciones);
        }

        private async void CargarCalificaciones()
        {
            IsBusy = true;

            var resultadoApi = await DocNocApi.Citas.TraeReviewsDelDoctor(new ParaFiltroUsuario() { IdUsuario = _idMedico });

            if (resultadoApi.Error)
            {
                IsBusy = false;
                CargaInicial = false;
                return;
            }

            OrdenarCalificaciones(resultadoApi.Registros);

            //var dummyList = new List<ParaTraeReviewsDelDoctor>()
            //{
            //    new ParaTraeReviewsDelDoctor()
            //    {
            //        Rating = 5,
            //        Nombre = "John Doe",
            //        FechaRegistro = DateTime.Today.AddDays(-4),
            //        Comentario = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
            //    },
            //    new ParaTraeReviewsDelDoctor()
            //    {
            //        Rating = 3,
            //        Nombre = "Alan Smithee",
            //        FechaRegistro = DateTime.Today.AddDays(-9),
            //        Comentario = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            //    },
            //    new ParaTraeReviewsDelDoctor()
            //    {
            //        Rating = 5,
            //        Nombre = "Paolo Ortega",
            //        FechaRegistro = DateTime.Today.AddDays(-12),
            //        Comentario = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem."
            //    },
            //    new ParaTraeReviewsDelDoctor()
            //    {
            //        Rating = 4,
            //        Nombre = "Alan Smithee",
            //        FechaRegistro = DateTime.Today.AddDays(-24),
            //        Comentario = "Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?"
            //    }
            //};

            //OrdenarCalificaciones(dummyList);

            IsBusy = false;
            CargaInicial = false;
        }

        private void OrdenarCalificaciones(List<ParaTraeReviewsDelDoctor> registros)
        {
            if(registros is null)
            {
                Calificaciones = new List<ParaTraeReviewsDelDoctor>();
                TotalOpiniones = 0;
                return;
            }

            TotalOpiniones = registros.Count;

            if (registros.Count < 2)
            {
                Calificaciones = new List<ParaTraeReviewsDelDoctor>(registros);
                return;
            }

            if (SortAntiguedadSeleccionado)
            {
                registros.Sort((p, q) => q.FechaRegistro.CompareTo(p.FechaRegistro));
                Calificaciones = new List<ParaTraeReviewsDelDoctor>(registros);
                return;
            }

            if (SortRatingSeleccionado)
            {
                registros.Sort((p, q) => q.Rating.CompareTo(p.Rating));
                Calificaciones = new List<ParaTraeReviewsDelDoctor>(registros);
                return;
            }
        }

        protected void VerDetalleComentario(ParaTraeReviewsDelDoctor comentario)
        {
            Comentario = comentario;
            DetalleVisible = true;
        }

        public void OnAppearing()
        {
            CargaInicial = true;
            _idMedico = Preferences.Get("IdMedico_Calificaciones");
            TituloPagina = "Opiniones: " + Preferences.Get("NombreMedico_Calificaciones");
            CargarCalificaciones();
        }

        #endregion
    }
}
