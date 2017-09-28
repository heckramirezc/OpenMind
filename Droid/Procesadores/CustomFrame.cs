using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OpenMind.ControlesPersonalizados;
using OpenMind.Droid.Procesadores;
using Java.Util;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace OpenMind.Droid.Procesadores
{
	public class CustomFrameRenderer : VisualElementRenderer<CustomFrame>
	{
		private GradientDrawable _normal, _pressed;

		protected override void OnElementChanged(ElementChangedEventArgs<CustomFrame> e)
		{

			CustomFrame customFram = e.NewElement as CustomFrame;
			// Create a drawable for the button's normal state
			_normal = new Android.Graphics.Drawables.GradientDrawable();
			_normal.SetColor(customFram.BackgroundColor.ToAndroid());
			_normal.SetStroke(customFram.BorderWidth, customFram.OutlineColor.ToAndroid());
			_normal.SetCornerRadius(customFram.BorderRadius);
			SetBackgroundDrawable(_normal);
			//SetBackgroundColor(customFram.BackgroundColor.ToAndroid());
			base.OnElementChanged(e);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
		}
	}
}