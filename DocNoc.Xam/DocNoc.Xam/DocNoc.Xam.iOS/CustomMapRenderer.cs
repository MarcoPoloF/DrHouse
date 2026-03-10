using System;
using System.Collections.Generic;
using System.ComponentModel;
using CoreGraphics;
using DocNoc.Xam.Custom.Maps;
using DocNoc.Xam.iOS;
using Foundation;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace DocNoc.Xam.iOS
{
    public class CustomMapRenderer : MapRenderer
    {
        UIView customPinView;
        List<CustomPin> customPins;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation = null;
                nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
            }
            
            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                //customPins = new List<CustomPin>(formsMap.MapPins);
                if (formsMap.MapPins != null)
                {
                    customPins = new List<CustomPin>(formsMap.MapPins);
                }
                else
                {
                    customPins = new List<CustomPin>();
                }

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            //if (annotation is MKUserLocation)
            //    return null;

            var anno = annotation as MKPointAnnotation;
            if (anno == null)
                return null;

            if (customPins.Count == 0)
                return null;

            var customPin = GetCustomPin(anno);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            annotationView = mapView.DequeueReusableAnnotation(customPin.IdMedico);
            if (annotationView == null)
            {
                anno.Title = "Ver Perfil";
                //anno.Title = customPin.NombreMedico;
                //anno.Subtitle = customPin.NombreEspecialidad;

                annotationView = new CustomMKAnnotationView(anno, customPin.IdMedico);
                annotationView.Image = UIImage.FromFile("pin.png");
                annotationView.CalloutOffset = new CGPoint(0, 0);
                //annotationView.BackgroundColor = UIColor.White;
                //annotationView.TintColor = UIColor.Black;

                annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                ((CustomMKAnnotationView)annotationView).Id = customPin.IdMedico;
                ((CustomMKAnnotationView)annotationView).Name = customPin.NombreMedico;
                ((CustomMKAnnotationView)annotationView).Discipline = customPin.NombreEspecialidad;
                ((CustomMKAnnotationView)annotationView).ImageUrl = customPin.RutaImagen;
            }
            annotationView.CanShowCallout = true;

            return annotationView;
        }

        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            CustomMKAnnotationView customView = e.View as CustomMKAnnotationView;
            
            var customPin = GetCustomPinById(customView.Id);

            ((CustomMap)Element).PinSelected(customPin);

            //if (!string.IsNullOrWhiteSpace(customView.ImageUrl))
            //{
            //    UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.ImageUrl));
            //}
        }

        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            CustomMKAnnotationView customView = e.View as CustomMKAnnotationView;

            //Tarjeta de médico.
            customPinView = new UIView();
            customPinView.Frame = new CGRect(0, 0, 200, 180);
            customPinView.BackgroundColor = UIColor.White;
            customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 100));

            //Foto del médico.
            var image = new UIImageView(new CGRect(60, 20, 80, 80));
            UIImage photo;
            using (var url = new NSUrl(customView.ImageUrl))
            using (var data = NSData.FromUrl(url))
                photo = UIImage.LoadFromData(data);
            image.Image = photo;
            customPinView.AddSubview(image);

            //Nombre del médico.
            var nombre = new UILabel(new CGRect(0, 110, 200, 20));
            nombre.Text = customView.Name;
            nombre.TextAlignment = UITextAlignment.Center;
            nombre.TextColor = UIColor.Black;
            customPinView.AddSubview(nombre);

            //Especialidad del médico.
            var especialidad = new UILabel(new CGRect(0, 140, 200, 20));
            especialidad.Text = customView.Discipline;
            especialidad.TextAlignment = UITextAlignment.Center;
            especialidad.TextColor = UIColor.Black;
            customPinView.AddSubview(especialidad);

            e.View.AddSubview(customPinView);
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
        }

        CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        CustomPin GetCustomPinById(string id)
        {
            foreach (var pin in customPins)
            {
                if (pin.IdMedico == id)
                {
                    return pin;
                }
            }
            return null;
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "MapPins")
            {
                var mapSender = (CustomMap)sender;
                if (mapSender.MapPins != null)
                {
                    customPins = new List<CustomPin>(mapSender.MapPins);
                }
            }
        }
    }
}