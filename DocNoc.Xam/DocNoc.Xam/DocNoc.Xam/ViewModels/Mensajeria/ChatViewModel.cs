using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.Chat;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Networking;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Mensajeria
{
    /// <summary>
    /// Definición de ViewModel: Chat (dn-71-3).
    /// </summary>
    public class ChatViewModel : DocNocViewModel
    {
        #region Constructor

        public ChatViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            this.BackCommand = new Command(Regresar);
            this.PopupCommand = new Command<SfPopup>(AbrirPopup);
            //this.LoadNewMessagesCommand = new Command(LoadNewMessages);
            this.LoadOldMessagesCommand = new Command(LoadOldMessages);
            this.SendMessageCommand = new Command<SendMessageEventArgs>(SendMessage);

            idOldestMessage = 0;
            idNewestMessage = 0;
            ChatHeight = DeviceDisplay.MainDisplayInfo.Height - 300;
        }

        #endregion

        #region Fields

        private string idDestinatario;

        private int idNewestMessage;

        private int idOldestMessage;

        #endregion

        #region Properties

        public double ChatHeight
        {
            get { return this._chatHeight; }
            set { SetProperty(ref _chatHeight, value); }
        }
        private double _chatHeight;

        public Author ChatUser
        {
            get { return this.chatUser; }
            set { SetProperty(ref chatUser, value); }
        }
        private Author chatUser;

        public Author Contact
        {
            get { return this.contact; }
            set { SetProperty(ref contact, value); }
        }
        private Author contact;

        public ObservableCollection<object> Messages
        {
            get { return this.messages; }
            set { SetProperty(ref messages, value); }
        }
        private ObservableCollection<object> messages;

        public string NombreContacto
        {
            get { return this.nombreContacto; }
            set { SetProperty(ref nombreContacto, value); }
        }
        private string nombreContacto;

        #endregion

        #region Commands

        public Command LoadNewMessagesCommand { get; set; }

        public Command LoadOldMessagesCommand { get; set; }

        public Command SendMessageCommand { get; set; }

        #endregion

        #region Methods

        private async void LoadChatUser()
        {
            var resultadoApi = await DocNocApi.Usuarios.TraeUsuario(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            ChatUser = new Author()
            {
                Name = "Yo",
                Avatar = resultadoApi.Contenido.RutaImagen
            };
        }

        private async void LoadContact()
        {
            var resultadoApi = await DocNocApi.Usuarios.TraeUsuarioProAPP(new ParaFiltroUsuario() { IdUsuario = idDestinatario });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            NombreContacto = resultadoApi.TituloNombreCompleto;

            Contact = new Author()
            {
                Name = resultadoApi.NombreCompleto,
                Avatar = resultadoApi.RutaImagen
            };
        }

        private async void LoadMessages()
        {
            Messages = new ObservableCollection<object>();

            var content = new ParaFiltroLeeConversacionAPP()
            {
                IdUsuarioEnvia = idUsuario,
                IdUsuarioRecibe = idDestinatario
            };

            var content1 = new ParaFiltroUsuarios()
            {
                IdUsuarioPaciente = idUsuario,
                IdUsuario = idDestinatario
            };

            var estableceLeidos = await DocNocApi.Mensajes.EstableceMensajesEnLeido(content1);

            var resultadoApi = await DocNocApi.Mensajes.LeeConversacionAPP(content);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            foreach(var registro in resultadoApi.Registros)
            {
                if(registro.IdUsuarioEnvia == idUsuario)
                {
                    Messages.Add(new TextMessage()
                    {
                        Author = ChatUser,
                        DateTime = registro.FechaEnvio,
                        Text = registro.TextoMensaje
                    });
                }
                else
                {
                    Messages.Add(new TextMessage()
                    {
                        Author = Contact,
                        DateTime = registro.FechaEnvio,
                        Text = registro.TextoMensaje
                    });
                }                
            }

            if (resultadoApi.Registros.Count > 0)
            {
                idOldestMessage = resultadoApi.Registros.First().IdMensaje;
                idNewestMessage = resultadoApi.Registros.Last().IdMensaje;
            }

            RefreshChat();
        }

        private async void LoadOldMessages()
        {
            //If the chat has no messages, the LoadMessages method is called instead.
            if (Messages.Count == 0)
            {
                LoadMessages();
                return;
            }

            var content = new ParaFiltroLeeConversacionAPP()
            {
                IdUsuarioEnvia = idUsuario,
                IdUsuarioRecibe = idDestinatario,
                IdMensaje = idOldestMessage
            };

            var resultadoApi = await DocNocApi.Mensajes.LeeConversacionAPP1(content);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            foreach (var registro in resultadoApi.Registros)
            {
                if (registro.IdUsuarioEnvia == idUsuario)
                {
                    Messages.Insert(0, new TextMessage()
                    {
                        Author = ChatUser,
                        DateTime = registro.FechaEnvio,
                        Text = registro.TextoMensaje
                    });
                }
                else
                {
                    Messages.Insert(0, new TextMessage()
                    {
                        Author = Contact,
                        DateTime = registro.FechaEnvio,
                        Text = registro.TextoMensaje
                    });
                }
            }

            if (resultadoApi.Registros.Count > 0)
            {
                idOldestMessage = resultadoApi.Registros.First().IdMensaje;
            }
        }

        private async void LoadNewMessages(bool autoRefresh = false)
        {
            //If the chat has no messages, the LoadMessages method is called instead.
            if(Messages.Count == 0)
            {
                LoadMessages();
                return;
            }

            var content = new ParaFiltroLeeConversacionAPP()
            {
                IdUsuarioEnvia = idUsuario,
                IdUsuarioRecibe = idDestinatario,
                IdMensaje = idNewestMessage
            };

            var resultadoApi = await DocNocApi.Mensajes.LeeConversacionAPP2(content);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                Navigation.NavigateBack();
                return;
            }

            foreach (var registro in resultadoApi.Registros)
            {
                if (registro.IdUsuarioEnvia != idUsuario)
                {
                    Messages.Add(new TextMessage()
                    {
                        Author = Contact,
                        DateTime = registro.FechaEnvio,
                        Text = registro.TextoMensaje
                    });
                }
            }

            if (resultadoApi.Registros.Count > 0)
            {
                idNewestMessage = resultadoApi.Registros.Last().IdMensaje;
            }

            if (autoRefresh)
            {
                RefreshChat();
            }
        }

        private async void RefreshChat()
        {
            await Task.Delay(TimeSpan.FromMinutes(1));

            LoadNewMessages(true);
        }

        private async void SendMessage(SendMessageEventArgs arguments)
        {
            var mensaje = new ParaEnvioMensajeUnico()
            {
                IdUsuarioEnvia = idUsuario,
                IdUsuarioRecibe = idDestinatario,
                TextoMensaje = arguments.Message.Text
            };

            var resultadoApi = await DocNocApi.Mensajes.EnviarMensajeUnico(mensaje);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
            }
        }

        public void OnAppearing()
        {
            idDestinatario = Preferences.Get("IdUsuario_Chat");
            LoadChatUser();
            LoadContact();
            LoadMessages();
        }

        #endregion
    }
}


