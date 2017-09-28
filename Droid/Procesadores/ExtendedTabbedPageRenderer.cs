using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using OpenMind.Droid.Procesadores;
using Android.Graphics.Drawables;


[assembly: ExportRenderer(typeof(TabbedPage), typeof(ExtendedTabbedPageRenderer))]
namespace OpenMind.Droid.Procesadores
{
	public class ExtendedTabbedPageRenderer : TabbedPageRenderer
	{
		public static void Init()
		{
			var test = DateTime.UtcNow;
		}

		Android.Graphics.Color _selectedColor = Xamarin.Forms.Color.FromHex("3E1152").ToAndroid();
		private static readonly Android.Graphics.Color DefaultUnselectedColor = Xamarin.Forms.Color.DarkGray.ToAndroid();
		private static Android.Graphics.Color BarBackgroundDefault;
		private Android.Graphics.Color _unselectedColor = DefaultUnselectedColor;

		ViewPager _viewPager;
		TabLayout _tabLayout;

		protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
		{

			base.OnElementChanged(e);

			// Get tabs

			for (int i = 0; i < ChildCount; i++)
			{
				var v = GetChildAt(i);
				if (v is ViewPager)
					_viewPager = (ViewPager)v;
                
				else if (v is TabLayout)
				{
					_tabLayout = (TabLayout)v;
					//_tabLayout.GetTabAt(i).SetText("HOLA");
				}
			}

            
			if (e.OldElement != null)
			{
				_tabLayout.TabSelected -= TabLayout_TabSelected;
				_tabLayout.TabUnselected -= TabLayout_TabUnselected;
			}

			if (e.NewElement != null)
			{
				BarBackgroundDefault = (_tabLayout.Background as ColorDrawable)?.Color ?? Xamarin.Forms.Color.FromHex("3E1152").ToAndroid();
				_tabLayout.TabSelected += TabLayout_TabSelected;
				_tabLayout.TabUnselected += TabLayout_TabUnselected;

				SetupTabColors();
				SelectTab(0);
			}

		}

		void SelectTab(int position)
		{
			if (_tabLayout.TabCount > position)
			{
				_tabLayout.GetTabAt(position).Icon?.SetColorFilter(_selectedColor, PorterDuff.Mode.SrcIn);
				_tabLayout.GetTabAt(position).Select();
			}
			else
			{
				throw new IndexOutOfRangeException();
			}
		}


		void SetupTabColors()
		{
			_tabLayout.SetSelectedTabIndicatorColor(_selectedColor);
			_tabLayout.SetTabTextColors(_unselectedColor, _selectedColor);
			for (int i = 0; i < _tabLayout.TabCount; i++)
			{
				var tab = _tabLayout.GetTabAt(i);
				tab.Icon?.SetColorFilter(_unselectedColor, PorterDuff.Mode.SrcIn);
			}
		}

		private void TabLayout_TabUnselected(object sender, TabLayout.TabUnselectedEventArgs e)
		{
			var tab = e.Tab;
			tab.Icon?.SetColorFilter(_unselectedColor, PorterDuff.Mode.SrcIn);
		}

		private void TabLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
		{
			var tab = e.Tab;
			_viewPager.CurrentItem = tab.Position;
			tab.Icon?.SetColorFilter(_selectedColor, PorterDuff.Mode.SrcIn);
		}

	}
}