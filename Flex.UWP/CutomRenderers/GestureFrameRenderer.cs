﻿using Flex.Extensions;
using Flex.UWP.CustomRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Frame), typeof(GestureFrameRenderer))]
namespace Flex.UWP.CustomRenderers
{
    public class GestureFrameRenderer : FrameRenderer
    {
        bool pressed;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            // Fix Xamarin.Forms Frame Bugs on UWP
            if (e.PropertyName.Equals("BackgroundColor"))
                FixFormsBackgroundColor((Xamarin.Forms.Frame)sender);            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                FixFormsBackgroundColor(e.NewElement);

                if (!e.NewElement.GestureRecognizers.Any())
                    return;

                if (!e.NewElement.GestureRecognizers.Any(x => x.GetType() == typeof(TouchGestureRecognizer)))
                    return;

                Control.PointerPressed += Control_PointerPressed;
                Control.PointerReleased += Control_PointerReleased;
                Control.PointerCanceled += Control_PointerReleased;
                Control.PointerCaptureLost += Control_PointerReleased;
                Control.PointerExited += Control_PointerReleased;

                // Fix Xamarin.Forms Frame Bugs on UWP
                Control.SizeChanged += (s, a) => FixFormsBackgroundColor(Element);
            }
        }

        /// <summary>
        /// On UWP, Xamarin.Forms uses a Border control to render its Frame elements
        /// Only the gods know, why they chose to render another Panel control around the Border and give
        /// BackgroundColor to the Panel instead of the Border directly. This avoids Border Radius to work properly
        /// and confuses touch events. Here is a workaround to fix that.
        /// </summary>
        /// <param name="frame"></param>
        private void FixFormsBackgroundColor(Xamarin.Forms.Frame frame)
        {
            var color = new Windows.UI.Color
            {
                A = Convert.ToByte(frame.BackgroundColor.A * 255),
                R = Convert.ToByte(frame.BackgroundColor.R * 255),
                G = Convert.ToByte(frame.BackgroundColor.G * 255),
                B = Convert.ToByte(frame.BackgroundColor.B * 255)
            };

            if (Control.Parent is Panel parent)
            {
                parent.Background = null;
            }

            Control.Background = new SolidColorBrush(color);
        }

        private void Control_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            pressed = true;

            foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
            {
                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                {
                    touchGestureRecognizer?.TouchDown();
                }
            }
        }

        private void Control_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            foreach (var recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
            {
                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                {
                    // Only fire, if button has been pressed before and not when mouse pointer just leaves the control
                    if (pressed)
                        touchGestureRecognizer?.TouchUp();
                }
            }

            pressed = false;
        }
    }
}