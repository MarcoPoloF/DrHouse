using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Widget;
using DocNoc.Xam.Custom.Maps;
using DocNoc.Xam.Droid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using static Android.Graphics.Bitmap;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace DocNoc.Xam.Droid
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        public List<CustomPin> customPins;

        public CustomMapRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;

                if (formsMap.MapPins != null)
                {
                    customPins = new List<CustomPin>(formsMap.MapPins);
                }
                else
                {
                    customPins = new List<CustomPin>();
                }

                Control.GetMapAsync(this);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MapPins")
            {
                var mapSender = (CustomMap)sender;
                if (mapSender.MapPins != null)
                {
                    customPins = new List<CustomPin>(mapSender.MapPins);
                }
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            //marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
            return marker;
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Pin no encontrado.");
            }

            ((CustomMap)Element).PinSelected(customPin);
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;

                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                if (infoTitle != null)
                {
                    infoTitle.Text = customPin.NombreMedico;
                }

                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = customPin.NombreEspecialidad;
                }

                var infoLogo = view.FindViewById<ImageView>(Resource.Id.InfoWindowLogo);
                if (infoLogo != null && customPin.RutaImagen != null && customPin.RutaImagen != String.Empty)
                {
                    var imageBitmap = GetImageBitmapFromUrl(customPin.RutaImagen);
                    infoLogo.SetImageBitmap(imageBitmap);
                }

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            try
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                string b = ex.Message;

                int[] colore = new int[1] { -1 };

                imageBitmap = Bitmap.CreateBitmap(colore, 1, 1, Config.Argb8888);
            }


            return imageBitmap;
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            NativeMap.Clear();

            base.Dispose(disposing);
        }
    }
}