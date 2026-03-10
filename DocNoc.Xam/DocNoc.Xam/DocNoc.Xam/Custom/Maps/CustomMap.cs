using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace DocNoc.Xam.Custom.Maps
{
    public class CustomMap : Xamarin.Forms.Maps.Map
    {
        //Declaración de la BindableProperty que recibe la colección IEnumerable de Custom Pins.
        public static readonly BindableProperty MapPinsProperty = BindableProperty.Create("MapPins", typeof(IEnumerable<CustomPin>), typeof(CustomMap),
            null, propertyChanged: OnMapPinsChanged);

        //Colección IEnumerable de Custom Pins.
        public IEnumerable<CustomPin> MapPins
        {
            get { return (IEnumerable<CustomPin>)GetValue(MapPinsProperty); }
            set { SetValue(MapPinsProperty, value); }
        }

        //Constructor. Recibe un MapSpan como argumento.
        public CustomMap(MapSpan region) : base(region)
        {
            //
        }

        //Constructor básico.
        public CustomMap() : this(MapSpan.FromCenterAndRadius(new Position(0, 0), Distance.FromKilometers(5)))
        {
            LoadCurrentPosition();
        }

        static void OnMapPinsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as CustomMap;

            if (oldValue is INotifyCollectionChanged)
                (oldValue as INotifyCollectionChanged).CollectionChanged -= map.OnCollectionChanged;
            if (newValue is INotifyCollectionChanged)
                (newValue as INotifyCollectionChanged).CollectionChanged += map.OnCollectionChanged;

            map.OnCollectionChanged(map, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            if (newValue != null)
                map.OnCollectionChanged(map, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)newValue));
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
                Pins.Clear();

            if (e.OldItems != null)
            {
                foreach (CustomPin pin in e.OldItems)
                {
                    Pins.Remove(pin);
                    pin.PropertyChanged -= OnPropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (CustomPin pin in e.NewItems)
                {
                    Pins.Add(pin);
                    pin.PropertyChanged += OnPropertyChanged;
                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // We should be able to just replace the changed pin, but rebuild is required to force map refresh
            Pins.Clear();
            foreach (var pin in MapPins)
                Pins.Add(pin);
        }

        private async void LoadCurrentPosition()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if(status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status == PermissionStatus.Granted)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        Position currentPosition = new Position(location.Latitude, location.Longitude);
                        this.MoveToRegion(MapSpan.FromCenterAndRadius(currentPosition, Distance.FromKilometers(5)));
                    }
                }

                //var status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
                //var status1 = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                //if ((status != PermissionStatus.Granted) && (status1 != PermissionStatus.Granted))
                //{
                //    status = await Permissions.RequestAsync<Permissions.LocationAlways>();

                //    if (status != PermissionStatus.Granted)
                //    {
                //        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                //    }
                //}
                //else
                //{
                //    if ((status != PermissionStatus.Granted) && (status1 == PermissionStatus.Granted))
                //        status = status1;
                //}

                //if (status == PermissionStatus.Granted)
                //{
                //    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                //    var location = await Geolocation.GetLocationAsync(request);

                //    if (location != null)
                //    {
                //        Position currentPosition = new Position(location.Latitude, location.Longitude);
                //        this.MoveToRegion(MapSpan.FromCenterAndRadius(currentPosition, Distance.FromKilometers(5)));
                //    }
                //}
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("ERROR", $"Ocurrió un error al acceder al mapa: {ex.Message}", "Ok");
            }

        }
        public void PinSelected(CustomPin customPin)
        {
            if (PinSelection != null)
            {
                PinSelection(this, new SelectedItemChangedEventArgs(customPin, 0));
            }
        }

        public event EventHandler<SelectedItemChangedEventArgs> PinSelection;
    }
}
