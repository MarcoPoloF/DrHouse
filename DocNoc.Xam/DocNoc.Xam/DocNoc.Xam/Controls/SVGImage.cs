using System;
using System.IO;
using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.Controls
{
    /// <summary>
    /// This is a helper class to render the SVG files.
    /// </summary>
    public class SVGImage : ContentView
    {
        // Bindable property to set the SVG image path
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
          nameof(Source), typeof(string), typeof(SVGImage), default(string), propertyChanged: RedrawCanvas);

        private readonly SKCanvasView canvasView = new SKCanvasView();

        public SVGImage()
        {
            this.Padding = new Thickness(0);
            this.BackgroundColor = Colors.Transparent;
            this.Content = this.canvasView;
            this.canvasView.PaintSurface += this.CanvasView_PaintSurface;
        }

        // Property to set the SVG image path
        public string? Source
        {
            get => (string?)this.GetValue(SourceProperty);
            set => this.SetValue(SourceProperty, value);
        }

        /// <summary>
        /// Method to invalidate the canvas to update the image
        /// </summary>
        public static void RedrawCanvas(BindableObject bindable, object oldValue, object newValue)
        {
            SVGImage? sVGImage = bindable as SVGImage;
            sVGImage?.canvasView.InvalidateSurface();
        }

        /// <summary>
        /// This method updates the canvas area with the image
        /// </summary>
        private void CanvasView_PaintSurface(object? sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas skCanvas = args.Surface.Canvas;
            skCanvas.Clear();

            if (string.IsNullOrEmpty(this.Source))
            {
                return;
            }

            // Get the assembly information to access the local image
            var assembly = typeof(SVGImage).GetTypeInfo().Assembly.GetName();
            var resourceName = assembly.Name + ".Images." + Source;

            using var stream = typeof(SVGImage).GetTypeInfo().Assembly.GetManifestResourceStream(resourceName);
            if (stream == null) return;

            // Use SkiaSharp.Extended.UI.Maui's SVG support
            using var skSvg = new SkiaSharp.Extended.Svg.SKSvg();
            skSvg.Load(stream);

            if (skSvg.Picture == null) return;

            SKImageInfo imageInfo = args.Info;
            skCanvas.Translate(imageInfo.Width / 2f, imageInfo.Height / 2f);
            SKRect rectBounds = skSvg.ViewBox;
            float xRatio = imageInfo.Width / rectBounds.Width;
            float yRatio = imageInfo.Height / rectBounds.Height;
            float minRatio = Math.Min(xRatio, yRatio);
            skCanvas.Scale(minRatio);
            skCanvas.Translate(-rectBounds.MidX, -rectBounds.MidY);
            skCanvas.DrawPicture(skSvg.Picture);
        }
    }
}
