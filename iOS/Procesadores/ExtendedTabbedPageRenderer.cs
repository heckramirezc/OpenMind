﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using OpenMind.iOS.Procesadores;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ExtendedTabbedPageRenderer))]

namespace OpenMind.iOS.Procesadores
{
	public class ExtendedTabbedPageRenderer : TabbedRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			// Set Text Font for unselected tab states
			UITextAttributes normalTextAttributes = new UITextAttributes();
			normalTextAttributes.Font = UIFont.FromName("ChalkboardSE-Light", 0.0F); // unselected

			UITabBarItem.Appearance.SetTitleTextAttributes(normalTextAttributes, UIControlState.Normal);
			UITabBar.Appearance.SelectedImageTintColor = Color.FromHex("3E1152").ToUIColor(); ;


		}

		public override UIViewController SelectedViewController
		{
			get
			{
				UITextAttributes selectedTextAttributes = new UITextAttributes();
				selectedTextAttributes.Font = UIFont.FromName("ChalkboardSE-Bold", 0.0F); // SELECTED
				if (base.SelectedViewController != null)
				{
					base.SelectedViewController.TabBarItem.SetTitleTextAttributes(selectedTextAttributes, UIControlState.Normal);
				}
				return base.SelectedViewController;
			}
			set
			{
				base.SelectedViewController = value;

				foreach (UIViewController viewController in base.ViewControllers)
				{
					UITextAttributes normalTextAttributes = new UITextAttributes();
					normalTextAttributes.Font = UIFont.FromName("ChalkboardSE-Light", 0.0F); // unselected

					viewController.TabBarItem.SetTitleTextAttributes(normalTextAttributes, UIControlState.Normal);
				}
			}
		}
	}
}

