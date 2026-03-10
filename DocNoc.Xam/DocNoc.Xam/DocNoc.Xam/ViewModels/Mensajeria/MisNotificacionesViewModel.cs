using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Mensajeria
{
    /// <summary>
    /// Definición de ViewModel: Mis Notificaciones (dn-10-3).
    /// </summary>
    public class MisNotificacionesViewModel : DocNocViewModel
    {
        #region Constructor

        public MisNotificacionesViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.ArchivaNotificacionCommand = new Command<MensajeDifusionTexto>(ArchivaNotificacion);
            
            //this.DeleteCommand = new Command(EliminarNotificaciones);

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

        public List<MensajeDifusionTexto> Notificaciones
        {
            get { return this.notificaciones; }
            set { SetProperty(ref notificaciones, value); }
        }
        private List<MensajeDifusionTexto> notificaciones;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command DeleteCommand { get; set; }
        public Command ArchivaNotificacionCommand { get; set; }

        #endregion

        #region Methods

        private async void ArchivaNotificacion(MensajeDifusionTexto mensajeDifusionTexto)
        {
            var siNo = await App.Current.MainPage.DisplayAlert("Eliminar", "Quieres eliminar el mensaje", "Si", "No");
            if (siNo)
                await DocNocApi.Mensajes.EstableceEliminado(mensajeDifusionTexto.IdMensaje);
            CargarDatos();
        }

        //private async void EliminarNotificaciones()
        //{

        //}

        private async void CargarDatos()
        {
            var resultadoApi = await DocNocApi.Mensajes.MensajeDifusionTexto(new ParaFiltroUsuario() { IdUsuario = IdExpediente });

            if (resultadoApi.Error)
            {
                return;
            }

            //Notificaciones = new List<MensajeDifusionTexto>()
            //{
            //    new MensajeDifusionTexto() { TextoMensaje = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum." },
            //    new MensajeDifusionTexto() { TextoMensaje = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?" },
            //    new MensajeDifusionTexto() { TextoMensaje = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
            //    new MensajeDifusionTexto() { TextoMensaje = "Quo Vadis?" },
            //    new MensajeDifusionTexto() { TextoMensaje = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat." },
            //    new MensajeDifusionTexto() { TextoMensaje = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur." },
            //    new MensajeDifusionTexto() { TextoMensaje = "Homo Homini Lupus" }
            //};

            Notificaciones = new List<MensajeDifusionTexto>(resultadoApi.Registros);
        }

        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}


