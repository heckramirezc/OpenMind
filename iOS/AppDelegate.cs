using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using Plugin.Toasts;
using RoundedBoxView.Forms.Plugin.iOSUnified;
using UIKit;
using Xamarin.Forms;

namespace OpenMind.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
            RoundedBoxViewRenderer.Init();
			DependencyService.Register<ToastNotificatorImplementation>();			
			ToastNotificatorImplementation.Init();

			LoadApplication(new App());
			imprimirFuentes();
			return base.FinishedLaunching(app, options);
		}

		void imprimirFuentes()
		{
			var fontList = new StringBuilder();
			var familyNames = UIFont.FamilyNames;
			foreach (var familyName in familyNames)
			{
				fontList.Append(String.Format("Family: {0}\n", familyName));
				Console.WriteLine("Family: {0}\n", familyName);
				var fontNames = UIFont.FontNamesForFamilyName(familyName);
				foreach (var fontName in fontNames)
				{
					Console.WriteLine("\tFont: {0}\n", fontName);
					fontList.Append(String.Format("\tFont: {0}\n", fontName));
				}
			};
		}
		protected PCLThinCanvas.DummyClassForLoadAssembly _dummy1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected PCLThinCanvas.iOS.DummyClassForLoadAssembly _dummy2
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
