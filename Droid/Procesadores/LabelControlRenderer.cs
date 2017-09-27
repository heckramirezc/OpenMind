using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.ComponentModel;
using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using OpenMind.Droid;
using OpenMind;
using OpenMind.Helpers;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(Label), typeof(LabelControlRenderer))]

namespace OpenMind.Droid
{
	public class LabelControlRenderer : LabelRenderer
	{

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
			{
				try
				{
					var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.FontFamily + ".ttf");
					Control.Typeface = font;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}

