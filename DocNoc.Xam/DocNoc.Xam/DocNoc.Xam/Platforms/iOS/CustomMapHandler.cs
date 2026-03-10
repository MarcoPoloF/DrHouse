using CoreGraphics;
using DocNoc.Xam.Custom.Maps;
using Foundation;
using MapKit;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UIKit;

namespace DocNoc.Xam.Platforms.iOS
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

        protected override MKMapView CreatePlatformView()
        {
            var mapView = base.CreatePlatformView();
            mapView.GetViewForAnnotation = GetViewForAnnotation;
            mapView.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
            mapView.DidSelectAnnotationView += OnDidSelectAnnotationView;
            mapView.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            return mapView;
        }

        private MKAnnotationView? GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation is MKUserLocation)
                return null;

            var anno = annotation as MKPointAnnotation;
            if (anno == null)
                return null;

            if (CustomPins.Count == 0)
                return null;

            var customPin = GetCustomPin(anno);
            if (customPin == null)
                return null;

            var annotationView = mapView.DequeueReusableAnnotation(customPin.IdMedico);
            if (annotationView == null)
            {
                anno.Title = "Ver Perfil";
                annotationView = new CustomMKAnnotationView(anno, customPin.IdMedico);
                annotationView.Image = UIImage.FromFile("pin.png");
                annotationView.CalloutOffset = new CGPoint(0, 0);
                annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);

                var customView = (CustomMKAnnotationView)annotationView;
                customView.Id = customPin.IdMedico;
                customView.Name = customPin.NombreMedico;
                customView.Discipline = customPin.NombreEspecialidad;
                customView.ImageUrl = customPin.RutaImagen;
            }

            annotationView.CanShowCallout = true;
            return annotationView;
        }

        private void OnCalloutAccessoryControlTapped(object? sender, MKMapViewAccessoryTappedEventArgs e)
        {
            if (e.View is CustomMKAnnotationView customView)
            {
                var customPin = GetCustomPinById(customView.Id);
                if (customPin != null)
                {
                    (VirtualView as CustomMap)?.PinSelected(customPin);
                }
            }
        }

        private UIView? _customPinView;

        private void OnDidSelectAnnotationView(object? sender, MKAnnotationViewEventArgs e)
        {
            if (e.View is not CustomMKAnnotationView customView)
                return;

            _customPinView = new UIView
            {
                Frame = new CGRect(0, 0, 200, 180),
                BackgroundColor = UIColor.White,
                Center = new CGPoint(0, -(e.View.Frame.Height + 100))
            };

            var image = new UIImageView(new CGRect(60, 20, 80, 80));
            if (!string.IsNullOrEmpty(customView.ImageUrl))
            {
                using var url = new NSUrl(customView.ImageUrl);
                using var data = NSData.FromUrl(url);
                if (data != null)
                    image.Image = UIImage.LoadFromData(data);
            }
            _customPinView.AddSubview(image);

            var nombre = new UILabel(new CGRect(0, 110, 200, 20))
            {
                Text = customView.Name,
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.Black
            };
            _customPinView.AddSubview(nombre);

            var especialidad = new UILabel(new CGRect(0, 140, 200, 20))
            {
                Text = customView.Discipline,
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.Black
            };
            _customPinView.AddSubview(especialidad);

            e.View.AddSubview(_customPinView);
        }

        private void OnDidDeselectAnnotationView(object? sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected && _customPinView != null)
            {
                _customPinView.RemoveFromSuperview();
                _customPinView.Dispose();
                _customPinView = null;
            }
        }

        private CustomPin? GetCustomPin(MKPointAnnotation annotation)
        {
            foreach (var pin in CustomPins)
            {
                if (pin.Location?.Latitude == annotation.Coordinate.Latitude &&
                    pin.Location?.Longitude == annotation.Coordinate.Longitude)
                {
                    return pin;
                }
            }
            return null;
        }

        private CustomPin? GetCustomPinById(string? id)
        {
            foreach (var pin in CustomPins)
            {
                if (pin.IdMedico == id)
                    return pin;
            }
            return null;
        }
    }
}
