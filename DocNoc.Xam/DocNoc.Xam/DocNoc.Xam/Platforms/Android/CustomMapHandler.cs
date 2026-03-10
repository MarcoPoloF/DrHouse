using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Widget;
using DocNoc.Xam.Custom.Maps;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

namespace DocNoc.Xam.Platforms.Android
{
    public class CustomMapHandler : MapHandler
    {
        public static readonly IPropertyMapper<CustomMap, CustomMapHandler> CustomMapper =
            new PropertyMapper<CustomMap, CustomMapHandler>(Mapper)
            {
                [nameof(CustomMap.MapPins)] = MapCustomPins,
            };

        public List<CustomPin> CustomPins { get; private set; } = new();

        public CustomMapHandler() : base(CustomMapper)
        {
        }

        private static void MapCustomPins(CustomMapHandler handler, CustomMap map)
        {
            handler.CustomPins = map.MapPins != null
                ? new List<CustomPin>(map.MapPins)
                : new List<CustomPin>();
        }

        protected override void ConnectHandler(MapView platformView)
        {
            base.ConnectHandler(platformView);
            platformView.GetMapAsync(new MapReadyCallback(this));
        }

        private class MapReadyCallback : Java.Lang.Object, IOnMapReadyCallback
        {
            private readonly CustomMapHandler _handler;

            public MapReadyCallback(CustomMapHandler handler)
            {
                _handler = handler;
            }

            public void OnMapReady(GoogleMap googleMap)
            {
                googleMap.SetInfoWindowAdapter(new CustomInfoWindowAdapter(_handler));
                googleMap.InfoWindowClick += (sender, e) =>
                {
                    var customPin = GetCustomPin(_handler.CustomPins, e.Marker);
                    if (customPin != null)
                    {
                        (_handler.VirtualView as CustomMap)?.PinSelected(customPin);
                    }
                };
            }
        }

        private static CustomPin? GetCustomPin(List<CustomPin> customPins, Marker marker)
        {
            var position = new Microsoft.Maui.Controls.Maps.Location(marker.Position.Latitude, marker.Position.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Location?.Latitude == position.Latitude &&
                    pin.Location?.Longitude == position.Longitude)
                {
                    return pin;
                }
            }
            return null;
        }

        private class CustomInfoWindowAdapter : Java.Lang.Object, GoogleMap.IInfoWindowAdapter
        {
            private readonly CustomMapHandler _handler;

            public CustomInfoWindowAdapter(CustomMapHandler handler)
            {
                _handler = handler;
            }

            public global::Android.Views.View? GetInfoContents(Marker marker)
            {
                var inflater = global::Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as global::Android.Views.LayoutInflater;
                if (inflater == null) return null;

                var customPin = GetCustomPin(_handler.CustomPins, marker);
                if (customPin == null) return null;

                var view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);
                if (view == null) return null;

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                if (infoTitle != null)
                    infoTitle.Text = customPin.NombreMedico;

                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);
                if (infoSubtitle != null)
                    infoSubtitle.Text = customPin.NombreEspecialidad;

                var infoLogo = view.FindViewById<ImageView>(Resource.Id.InfoWindowLogo);
                if (infoLogo != null && !string.IsNullOrEmpty(customPin.RutaImagen))
                {
                    var imageBitmap = GetImageBitmapFromUrl(customPin.RutaImagen);
                    if (imageBitmap != null)
                        infoLogo.SetImageBitmap(imageBitmap);
                }

                return view;
            }

            public global::Android.Views.View? GetInfoWindow(Marker marker) => null;

            private static Bitmap? GetImageBitmapFromUrl(string url)
            {
                try
                {
#pragma warning disable SYSLIB0014
                    using var webClient = new WebClient();
#pragma warning restore SYSLIB0014
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes?.Length > 0)
                        return BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
                catch
                {
                    int[] color = new int[1] { -1 };
                    return Bitmap.CreateBitmap(color, 1, 1, Bitmap.Config.Argb8888!);
                }
                return null;
            }
        }
    }
}
