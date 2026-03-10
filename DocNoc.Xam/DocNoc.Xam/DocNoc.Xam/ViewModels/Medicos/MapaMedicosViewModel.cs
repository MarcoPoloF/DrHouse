using DocNoc.Models;
using DocNoc.Xam.Custom.Maps;
using DocNoc.Xam.Enum.Sort;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;

namespace DocNoc.Xam.ViewModels.Medicos
{
    /// <summary>
    /// Definición de ViewModel: Mapa de Médicos (dn-13-3).
    /// </summary>
    public class MapaMedicosViewModel : DocNocViewModel
    {
        #region Constructor

        public MapaMedicosViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.PinSeleccionadoCommand = new Command<object>(NavegarPerfilMedico);

            Pins = new List<CustomPin>();
        }

        #endregion

        #region Properties

        public List<CustomPin> Pins
        {
            get { return this.pins; }
            set { SetProperty(ref pins, value); }
        }
        private List<CustomPin> pins;

        #endregion

        #region Commands

        public Command PinSeleccionadoCommand { get; set; }

        #endregion

        #region Methods

        private async void CargarDatos()
        {
            var resultadoApi = await DocNocApi.Citas.BuscaMedico3();

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            var listaPines = new List<CustomPin>();

            foreach(var registro in resultadoApi.Registros)
            {
                if (!string.IsNullOrWhiteSpace(registro.Latitud) && !string.IsNullOrWhiteSpace(registro.Longitud))
                {
                    double.TryParse(registro.Latitud, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out double latitud);
                    double.TryParse(registro.Longitud, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out double longitud);

                    listaPines.Add(new CustomPin()
                    {
                        Label = registro.NombreCompletoTitulo,
                        Position = AdjustPinPosition(listaPines, new Position(latitud, longitud)),
                        IdMedico = registro.IdUsuario,
                        NombreMedico = registro.NombreCompletoTitulo,
                        NombreEspecialidad = registro.NombreEspecialidad,
                        RutaImagen = registro.RutaImagen
                    });
                }
            }

            Pins = new List<CustomPin>(listaPines);
        }

        private Position AdjustPinPosition(List<CustomPin> pinList, Position pinPosition, bool firstCheck = true)
        {
            if(pinList.Exists(x => x.Position == pinPosition))
            {
                var newLatitude = RandomPositionChanger(pinPosition.Latitude);
                var newLongitude = RandomPositionChanger(pinPosition.Longitude);
                var newPinPosition = new Position(newLatitude, newLongitude);
                return AdjustPinPosition(pinList, newPinPosition, false);
            }

            return pinPosition;
        }

        private double[] randomDoubles = { 0.0000001, -0.0000001, 0.0000001, -0.0000001, 0.0000002, -0.0000002, 0.0000003, -0.0000003 };

        private double RandomPositionChanger(double coordinate)
        {
            Random rnd = new Random();

            return coordinate + randomDoubles[rnd.Next(8)];
        }

        private void NavegarAgendarCita(ResultadoBusquedaMedicoAPP medico)
        {
            Preferences.Set("PerfilMedico_Id", medico.IdUsuario);

            //Navegación a página "Agendar Cita" (dn-19-3).
            Navigation.NavigateTo(typeof(ViewModels.Citas.MyAddressViewModel), string.Empty, string.Empty);
        }

        private void NavegarPerfilMedico(object medico)
        {
            var infoMedico = (CustomPin)medico;

            Preferences.Set("PerfilMedico_Id", infoMedico.IdMedico);

            Navigation.NavigateTo(typeof(ViewModels.Medicos.ContactProfileViewModel), string.Empty, string.Empty);
        }

        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}


